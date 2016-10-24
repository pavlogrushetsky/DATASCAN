using System;
using System.Collections.Generic;
using System.Linq;
using DATASCAN.Connection.Services;
using DATASCAN.Infrastructure.Logging;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Scanning;
using DATASCAN.Services;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Scanners
{
    public class FloutecScanner : ScannerBase
    {
        private string _dbfConnection;
        private FloutecDbfService _dbfService;
        private FloutecDataService _dataService;

        public FloutecScanner(LogListView log) : base(log)
        {                        
        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _connection = connection;
            _members = members.ToList();
            _estimators = estimators.ToList();
            _dbfConnection = Infrastructure.Settings.Settings.DbfPath;
            _dbfService = new FloutecDbfService(_dbfConnection);
            _dataService = new FloutecDataService(_connection);

            _members.ForEach(m =>
            {
                var member = m as FloutecScanMember;
                if (member == null)
                    return;
                var floutec = _estimators.SingleOrDefault(e => e.Id == member.EstimatorId) as Floutec;
                if (floutec == null)
                    return;
                floutec.MeasurePoints.Where(p => p.IsActive).ToList().ForEach(point =>
                {
                    var line = point as FloutecMeasureLine;
                    if (line == null)
                        return;
                    ProcessLine(member, floutec, line);
                });
            });                    
        }

        private void ProcessLine(FloutecScanMember member, Floutec floutec, FloutecMeasureLine line)
        {
            if (member.ScanIdentData)
                ScanIdentData(floutec, line);
            if (member.ScanInterData)
                ScanInterData(floutec, line);
            if (member.ScanAlarmData)
                ScanAlarmData(floutec, line);
            if (member.ScanHourlyData)
                ScanHourlyData(floutec, line);
            if (member.ScanInstantData)
                ScanInstantData(floutec, line);
        }

        private async void ScanIdentData(Floutec floutec, FloutecMeasureLine line)
        {
            await _dbfService.GetIdentData(floutec.Address, line.Number, async data =>
            {
                if (data == null)
                {
                    Logger.Log(_log, new LogEntry { Message = $"Дані ідентифікації нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Warning, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    return;
                }

                data.N_FLONIT = floutec.Address*10 + line.Number;
                data.FloutecMeasureLineId = line.Id;

                await _dataService.SaveIdentData(data, saved =>
                {
                    Logger.Log(_log,
                        saved ? new LogEntry { Message = $"Дані ідентифікації нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} успішно оновлено", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now }
                              : new LogEntry { Message = $"Дані ідентифікації нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} не змінилися", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних ідентифікації нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання даних ідентифікації нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
            });                                    
        }

        private async void ScanInterData(Floutec floutec, FloutecMeasureLine line)
        {
            await _dbfService.GetInterData(floutec.Address, line.Number, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Дані втручань нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Warning, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.N_FLONIT = floutec.Address*10 + line.Number;
                    d.FloutecMeasureLineId = line.Id;
                });                

                await _dataService.SaveInterData(line.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Дані втручань нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові дані втручань нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних втручань нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання даних втручань нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
            });
        }

        private async void ScanAlarmData(Floutec floutec, FloutecMeasureLine line)
        {
            await _dbfService.GetAlarmData(floutec.Address, line.Number, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Дані аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Warning, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.N_FLONIT = floutec.Address*10 + line.Number;
                    d.FloutecMeasureLineId = line.Id;
                });

                await _dataService.SaveAlarmData(line.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Дані аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові дані аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання даних аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
            });
        }

        private async void ScanHourlyData(Floutec floutec, FloutecMeasureLine line)
        {
            await _dbfService.GetHourlyData(floutec.Address, line.Number, async data =>
            {
                if (data == null || !data.Any())
                {
                    Logger.Log(_log, new LogEntry { Message = $"Годинні дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Warning, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    return;
                }

                data.ForEach(d =>
                {
                    d.N_FLONIT = floutec.Address * 10 + line.Number;
                    d.FloutecMeasureLineId = line.Id;
                });

                await _dataService.SaveHourlyData(line.Id, data, saved =>
                {
                    Logger.Log(_log,
                        saved > 0
                            ? new LogEntry { Message = $"Годинні дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} успішно оновлено. Додано записів: {saved}", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now }
                            : new LogEntry { Message = $"Нові годинні дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження даних аварій нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання годинних даних нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
            });
        }

        private async void ScanInstantData(Floutec floutec, FloutecMeasureLine line)
        {
            await _dbfService.GetInstantData(floutec.Address, line.Number, async data =>
            {
                if (data == null)
                {
                    Logger.Log(_log, new LogEntry { Message = $"Миттєві дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Warning, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    return;
                }

                data.N_FLONIT = floutec.Address * 10 + line.Number;
                data.FloutecMeasureLineId = line.Id;

                await _dataService.SaveInstantData(data, saved =>
                {
                    Logger.Log(_log,
                        saved ? new LogEntry { Message = $"Миттєві дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} успішно оновлено", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now }
                              : new LogEntry { Message = $"Нові миттєві дані нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address} відсутні", Status = LogStatus.Success, Type = LogType.Floutec, Timestamp = DateTime.Now });
                }, ex =>
                {
                    Logger.Log(_log, new LogEntry { Message = $"Помилка збереження миттєвих даних нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                    Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                });
            }, ex =>
            {
                Logger.Log(_log, new LogEntry { Message = $"Помилка читання миттєвих даних нитки №{line.Number} обчислювача ФЛОУТЕК з адресою {floutec.Address}", Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
                Logger.Log(_log, new LogEntry { Message = ex.Message, Status = LogStatus.Error, Type = LogType.Floutec, Timestamp = DateTime.Now });
            });
        }
    }
}