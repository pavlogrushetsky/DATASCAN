using System.Collections.Generic;
using System.Linq;
using DATASCAN.Core.Entities;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.Services;
using DATASCAN.View.Controls;

namespace DATASCAN.Scanners
{
    public class RocScanner : ScannerBase
    {
        private RocService _service;

        public RocScanner(LogListView log) : base(log)
        {

        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _connection = connection;
            _members = members.ToList();
            _estimators = estimators.ToList();
            _service = new RocService();

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

        private void ProcessPoint(RocScanMember member, Roc809 roc, Roc809MeasurePoint point)
        {
            if (member.ScanEventData)
                ScanEventData(roc);
            if (member.ScanAlarmData)
                ScanAlarmData(roc);
            if (member.ScanMinuteData)
                ScanMinuteData(roc, point);
            if (member.ScanPeriodicData)
                ScanPeriodicData(roc, point);
            if (member.ScanDailyData)
                ScanDailyData(roc, point);
        }

        private async void ScanEventData(Roc809 roc)
        {
            await _service.GetEventData(roc, data =>
            {

            }, ex =>
            {

            });
        }

        private async void ScanAlarmData(Roc809 roc)
        {
            await _service.GetAlarmData(roc, data =>
            {

            }, ex =>
            {

            });
        }

        private async void ScanMinuteData(Roc809 roc, Roc809MeasurePoint point)
        {
            await _service.GetMinuteData(roc, point, data =>
            {

            }, ex =>
            {

            });
        }

        private async void ScanPeriodicData(Roc809 roc, Roc809MeasurePoint point)
        {
            await _service.GetPeriodicData(roc, point, data =>
            {

            }, ex =>
            {

            });
        }

        private async void ScanDailyData(Roc809 roc, Roc809MeasurePoint point)
        {
            await _service.GetDailyData(roc, point, data =>
            {

            }, ex =>
            {

            });
        }
    }
}