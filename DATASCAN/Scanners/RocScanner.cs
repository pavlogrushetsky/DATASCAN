using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Communication.Clients;
using DATASCAN.Communication.Common;
using DATASCAN.Core.Entities;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Services;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Services;
using DATASCAN.View.Controls;

namespace DATASCAN.Scanners
{
    public class RocScanner : ScannerBase
    {
        private RocService _service;
        private RocDataService _dataService;

        private int _baudRate;
        private int _dataBits;
        private StopBits _stopBits;
        private Parity _parity;
        private Handshake _handshake;
        private int _readTimeout;
        private int _writeTimeout;

        public RocScanner(LogListView log) : base(log)
        {

        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _connection = connection;
            _members = members.ToList();
            _estimators = estimators.ToList();
            _service = new RocService();
            _dataService = new RocDataService(_connection);

            _baudRate = int.Parse(Settings.Baudrate);
            _dataBits = int.Parse(Settings.DataBits);
            _stopBits = (StopBits) Enum.Parse(typeof (StopBits), Settings.StopBits);
            _parity = (Parity) Enum.Parse(typeof (Parity), Settings.Parity);
            _handshake = Handshake.None;
            _readTimeout = 500;
            _writeTimeout = 500;

            _members.ForEach(async m =>
            {
                var member = m as RocScanMember;
                if (member == null)
                    return;
                var roc = _estimators.SingleOrDefault(e => e.Id == member.EstimatorId) as Roc809;
                if (roc == null)
                    return;

                IClient client;

                if (!roc.IsScannedViaGPRS)
                    client = new TcpIpClient(roc.Address, roc.Port);
                else
                {
                    var port = await GetFreeSerialPort();
                    if (!string.IsNullOrEmpty(port))
                        client = new GprsClient(roc.Phone, port, _baudRate, _parity, _dataBits, _stopBits);
                    else
                    { 
                        Logger.Log(_log, new LogEntry { Message = "Помилка виділення СОМ-порта для опитування. Порти відсутні або зайняті", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                        return;
                    }
                }

                if (member.ScanEventData || member.ScanAlarmData)
                {
                    ProcessRoc(member, roc, client);
                }

                roc.MeasurePoints.Where(p => p.IsActive).ToList().ForEach(p =>
                {
                    var point = p as Roc809MeasurePoint;
                    if (point == null)
                        return;
                    ProcessPoint(member, roc, point, client);
                });
            });
        }

        private void ProcessRoc(RocScanMember member, Roc809 roc, IClient client)
        {
            if (member.ScanEventData)
                ScanEventData(roc, client);
            if (member.ScanAlarmData)
                ScanAlarmData(roc, client);
        }

        private void ProcessPoint(RocScanMember member, Roc809 roc, Roc809MeasurePoint point, IClient client)
        {            
            if (member.ScanMinuteData)
                ScanMinuteData(roc, point, client);
            if (member.ScanPeriodicData)
                ScanPeriodicData(roc, point, client);
            if (member.ScanDailyData)
                ScanDailyData(roc, point, client);
        }

        private Task<string> GetFreeSerialPort()
        {
            var ports = new List<string>();
            var statuses = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Settings.COMPort1))
                ports.Add(Settings.COMPort1);
            if (!string.IsNullOrEmpty(Settings.COMPort2))
                ports.Add(Settings.COMPort2);
            if (!string.IsNullOrEmpty(Settings.COMPort3))
                ports.Add(Settings.COMPort3);

            var start = DateTime.Now;

            return Task.Factory.StartNew(() =>
            {
                do
                {
                    ports.ForEach(p =>
                    {
                        try
                        {
                            SerialPortFixer.Execute(p);
                            using (var port = new SerialPort
                            {
                                PortName = p,
                                BaudRate = _baudRate,
                                DataBits = _dataBits,
                                StopBits = _stopBits,
                                Parity = _parity,
                                Handshake = _handshake,
                                ReadTimeout = _readTimeout,
                                WriteTimeout = _writeTimeout
                            })
                            {
                                if (!port.IsOpen)
                                    port.Open();

                                port.WriteLine(@"ATQ0V1E0" + "\r\n");
                                Task.Delay(500);

                                var status = port.ReadExisting();
                                if (!statuses.ContainsKey(p))
                                    statuses.Add(p, status);
                                else
                                    statuses[p] = status;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    });

                    Task.Delay(500);
                } while (!statuses.ContainsValue("\r\nOK\r\n") || DateTime.Now < start.AddMilliseconds(500));

                return statuses.ContainsValue("\r\nOK\r\n") 
                    ? statuses.First(pair => pair.Value.Equals("\r\nOK\r\n")).Key 
                    : "";
            }, TaskCreationOptions.LongRunning);            
        }

        private async void ScanEventData(Roc809 roc, IClient client)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            await _service.GetEventData(client, roc, async data =>
            {                
                if (data == null || !data.Any())
                {                    
                    Logger.Log(_log, new LogEntry { Message = $"Дані подій обчислювача ROC809 з {s} відсутні", Status = LogStatus.Warning, Type = LogType.Roc, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.Roc809Id = roc.Id;
                });

                await _dataService.SaveEventData(roc.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Дані подій обчислювача ROC809 з {s} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові дані подій обчислювача ROC809 з {s} відсутні", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних подій обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання даних подій обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
            });
        }

        private async void ScanAlarmData(Roc809 roc, IClient client)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            await _service.GetAlarmData(client, roc, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Дані аварій обчислювача ROC809 з {s} відсутні", Status = LogStatus.Warning, Type = LogType.Roc, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.Roc809Id = roc.Id;
                });

                await _dataService.SaveAlarmData(roc.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Дані аварій обчислювача ROC809 з {s} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові дані аварій обчислювача ROC809 з {s} відсутні", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних аварій обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання даних аварій обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
            });
        }

        private async void ScanMinuteData(Roc809 roc, Roc809MeasurePoint point, IClient client)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            await _service.GetMinuteData(client, roc, point, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Хвилинні дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Warning, Type = LogType.Roc, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.Roc809MeasurePointId = point.Id;
                });

                await _dataService.SaveMinuteData(point.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Хвилинні дані точки №{point.Number} обчислювача ROC809 з {s} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові хвилинні дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження хвилинних даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання хвилинних даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
            });
        }

        private async void ScanPeriodicData(Roc809 roc, Roc809MeasurePoint point, IClient client)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            await _service.GetPeriodicData(client, roc, point, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Періодичні дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Warning, Type = LogType.Roc, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.Roc809MeasurePointId = point.Id;
                });

                await _dataService.SavePeriodicData(point.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Періодичні дані точки №{point.Number} обчислювача ROC809 з {s} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові періодичні дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження періодичних даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання періодичних даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
            });
        }

        private async void ScanDailyData(Roc809 roc, Roc809MeasurePoint point, IClient client)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            await _service.GetDailyData(client, roc, point, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Добові дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Warning, Type = LogType.Roc, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.Roc809MeasurePointId = point.Id;
                });

                await _dataService.SaveDailyData(point.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Добові дані точки №{point.Number} обчислювача ROC809 з {s} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові добові дані точки №{point.Number} обчислювача ROC809 з {s} відсутні", Status = LogStatus.Success, Type = LogType.Roc, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження добових даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання добових даних точки №{point.Number} обчислювача ROC809 з {s}", Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Roc, Timestamp = DateTime.Now });
            });
        }
    }
}