using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities;
using DATASCAN.Core.Entities.Floutecs;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    public class EstimatorsService : EntitiesService<EstimatorBase>
    {
        public EstimatorsService(string connection) : base(connection)
        {
        }

        public async Task Delete(EstimatorBase estimator, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<EstimatorBase>(_connection))
                using (var repoMinuteData = new DataRecordRepository<Roc809MinuteData>(_connection))
                using (var repoPeriodicData = new DataRecordRepository<Roc809PeriodicData>(_connection))
                using (var repoDailyData = new DataRecordRepository<Roc809DailyData>(_connection))
                using (var repoIdentData = new DataRecordRepository<FloutecIdentData>(_connection))
                using (var repoInstantData = new DataRecordRepository<FloutecInstantData>(_connection))
                using (var repoInterData = new DataRecordRepository<FloutecInterData>(_connection))
                using (var repoAlarmData = new DataRecordRepository<FloutecAlarmData>(_connection))
                using (var repoHourlyData = new DataRecordRepository<FloutecHourlyData>(_connection))
                {
                    if (estimator is Roc809)
                    {
                        var roc = repo.GetAll().Where(e => e.Id == estimator.Id).OfType<Roc809>().Include(e => e.AlarmData).Include(e => e.EventData).Include(e => e.MeasurePoints).Include(e => e.Scans).Single();

                        var minuteData = repoMinuteData.GetAll().ToList().Where(d => roc.MeasurePoints.Select(p => p.Id).Contains(d.Roc809MeasurePointId));
                        var periodicData = repoPeriodicData.GetAll().ToList().Where(d => roc.MeasurePoints.Select(p => p.Id).Contains(d.Roc809MeasurePointId));
                        var dailyData = repoDailyData.GetAll().ToList().Where(d => roc.MeasurePoints.Select(p => p.Id).Contains(d.Roc809MeasurePointId));

                        repoMinuteData.Delete(minuteData);
                        repoPeriodicData.Delete(periodicData);
                        repoDailyData.Delete(dailyData);

                        repo.Delete(new List<Roc809> { roc });
                    }
                    else
                    {
                        var est = repo.GetAll().Where(e => e.Id == estimator.Id).Include(e => e.MeasurePoints).Include(e => e.Scans).Single();

                        var identData = repoIdentData.GetAll().ToList().Where(d => est.MeasurePoints.Select(p => p.Id).Contains(d.FloutecMeasureLineId));
                        var instantData = repoInstantData.GetAll().ToList().Where(d => est.MeasurePoints.Select(p => p.Id).Contains(d.FloutecMeasureLineId));
                        var interData = repoInterData.GetAll().ToList().Where(d => est.MeasurePoints.Select(p => p.Id).Contains(d.FloutecMeasureLineId));
                        var alarmData = repoAlarmData.GetAll().ToList().Where(d => est.MeasurePoints.Select(p => p.Id).Contains(d.FloutecMeasureLineId));
                        var hourlyData = repoHourlyData.GetAll().ToList().Where(d => est.MeasurePoints.Select(p => p.Id).Contains(d.FloutecMeasureLineId));

                        repoIdentData.Delete(identData);
                        repoInstantData.Delete(instantData);
                        repoInterData.Delete(interData);
                        repoAlarmData.Delete(alarmData);
                        repoHourlyData.Delete(hourlyData);

                        repo.Delete(new List<EstimatorBase> { est });
                    }
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}