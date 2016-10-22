using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using DATASCAN.Connection.Services;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Model;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Scanners
{
    public class RocScanner : ScannerBase
    {
        private RocGprsService _gprsService;

        public RocScanner(LogListView log) : base(log)
        {

        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _connection = connection;
            _members = members.ToList();
            _estimators = estimators.ToList();

            var port = Settings.COMPort1;
            var baudrate = string.IsNullOrEmpty(Settings.Baudrate) ? 19200 : int.Parse(Settings.Baudrate);
            var dataBits = string.IsNullOrEmpty(Settings.DataBits) ? 8 : int.Parse(Settings.DataBits);
            var stopBits = string.IsNullOrEmpty(Settings.StopBits) ? StopBits.One : (StopBits) Enum.Parse(typeof (StopBits), Settings.StopBits);
            var parity = string.IsNullOrEmpty(Settings.Parity) ? Parity.Even : (Parity) Enum.Parse(typeof (Parity), Settings.Parity); 

            _gprsService = new RocGprsService(port, baudrate, parity, dataBits, stopBits);

            _members.ForEach(m =>
            {
                var member = m as RocScanMember;
                if (member == null)
                    return;
                var roc = _estimators.SingleOrDefault(e => e.Id == member.EstimatorId) as Roc809;
                if (roc == null)
                    return;
                roc.MeasurePoints.Where(p => p.IsActive).ToList().ForEach(p =>
                {
                    var point = p as Roc809MeasurePoint;
                    if (point == null)
                        return;
                    ProcessPoint(member, roc, point);
                });
            });
        }

        private void ProcessPoint(RocScanMember member, Roc809 floutec, Roc809MeasurePoint point)
        {
            if (member.ScanEventData)
                ScanEventData(floutec, point);
            if (member.ScanAlarmData)
                ScanAlarmData(floutec, point);
            if (member.ScanMinuteData)
                ScanMinuteData(floutec, point);
            if (member.ScanPeriodicData)
                ScanPeriodicData(floutec, point);
            if (member.ScanDailyData)
                ScanDailyData(floutec, point);
        }

        private async void ScanEventData(Roc809 roc, Roc809MeasurePoint point)
        {
            await _gprsService.GetEventData(roc, point, data =>
            {

            }, ex =>
            {

            });
        }

        private async void ScanAlarmData(Roc809 roc, Roc809MeasurePoint point)
        {

        }

        private async void ScanMinuteData(Roc809 roc, Roc809MeasurePoint point)
        {

        }

        private async void ScanPeriodicData(Roc809 roc, Roc809MeasurePoint point)
        {

        }

        private async void ScanDailyData(Roc809 roc, Roc809MeasurePoint point)
        {

        }
    }
}