using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DATASCAN.Connection.Services;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Scanners
{
    public class FloutecScanner : ScannerBase
    {
        private string _dbfConnection;
        private FloutecDbfService _dbfService;

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
                ScanIdentData(floutec.Address, line.Number);
            if (member.ScanInterData)
                ScanInterData(floutec.Address, line.Number);
            if (member.ScanAlarmData)
                ScanAlarmData(floutec.Address, line.Number);
            if (member.ScanHourlyData)
                ScanHourlyData(floutec.Address, line.Number);
            if (member.ScanInstantData)
                ScanInstantData(floutec.Address, line.Number);
        }

        private async void ScanIdentData(int address, int number)
        {
            await _dbfService.GetIdentData(address, number, data =>
            {
                Debug.WriteLine("Ident data was received.");              
            }, ex =>
            {
                Debug.WriteLine(ex.Message);
            });            
        }

        private async void ScanInterData(int address, int number)
        {
            await _dbfService.GetInterData(address, number, data =>
            {
                Debug.WriteLine("Inter data was received.");
            }, ex =>
            {
                Debug.WriteLine(ex.Message);
            });
        }

        private async void ScanAlarmData(int address, int number)
        {
            await _dbfService.GetAlarmData(address, number, data =>
            {
                Debug.WriteLine("Alarm data was received.");
            }, ex =>
            {
                Debug.WriteLine(ex.Message);
            });
        }

        private async void ScanHourlyData(int address, int number)
        {
            await _dbfService.GetHourlyData(address, number, data =>
            {
                Debug.WriteLine("Hourly data was received.");
            }, ex =>
            {
                Debug.WriteLine(ex.Message);
            });
        }

        private async void ScanInstantData(int address, int number)
        {
            await _dbfService.GetInstantData(address, number, data =>
            {
                Debug.WriteLine("Hourly data was received.");
            }, ex =>
            {
                Debug.WriteLine(ex.Message);
            });
        }
    }
}