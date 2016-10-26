using System;
using System.Collections.Generic;
using System.Linq;
using DATASCAN.Communication.Clients;
using DATASCAN.Core.Entities;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Services;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Services;
using DATASCAN.View.Controls;

namespace DATASCAN.Scanners
{
    public class RocScanner : ScannerBase
    {
        private RocService _service;
        private RocDataService _dataService;

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

            _members.ForEach(m =>
            {
                var member = m as RocScanMember;
                if (member == null)
                    return;
                var roc = _estimators.SingleOrDefault(e => e.Id == member.EstimatorId) as Roc809;
                if (roc == null)
                    return;
                if (member.ScanEventData || member.ScanAlarmData)
                {
                    ProcessRoc(member, roc);
                }

                roc.MeasurePoints.Where(p => p.IsActive).ToList().ForEach(p =>
                {
                    var point = p as Roc809MeasurePoint;
                    if (point == null)
                        return;
                    ProcessPoint(member, roc, point);
                });
            });
        }

        private void ProcessRoc(RocScanMember member, Roc809 roc)
        {
            if (member.ScanEventData)
                ScanEventData(roc);
            if (member.ScanAlarmData)
                ScanAlarmData(roc);
        }

        private void ProcessPoint(RocScanMember member, Roc809 roc, Roc809MeasurePoint point)
        {            
            if (member.ScanMinuteData)
                ScanMinuteData(roc, point);
            if (member.ScanPeriodicData)
                ScanPeriodicData(roc, point);
            if (member.ScanDailyData)
                ScanDailyData(roc, point);
        }

        private async void ScanEventData(Roc809 roc)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            var client = new TcpIpClient(roc.Address, roc.Port);

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

        private async void ScanAlarmData(Roc809 roc)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            var client = new TcpIpClient(roc.Address, roc.Port);

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

        private async void ScanMinuteData(Roc809 roc, Roc809MeasurePoint point)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            var client = new TcpIpClient(roc.Address, roc.Port);

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

        private async void ScanPeriodicData(Roc809 roc, Roc809MeasurePoint point)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            var client = new TcpIpClient(roc.Address, roc.Port);

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

        private async void ScanDailyData(Roc809 roc, Roc809MeasurePoint point)
        {
            var s = roc.IsScannedViaGPRS ? $"телефоном {roc.Phone}" : $"адресою {roc.Address}";

            var client = new TcpIpClient(roc.Address, roc.Port);

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