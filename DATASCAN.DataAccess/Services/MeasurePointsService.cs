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
    public class MeasurePointsService : EntitiesService<MeasurePointBase>
    {
        public MeasurePointsService(string connection) : base(connection)
        {
        }

        public async Task Delete(MeasurePointBase point, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<MeasurePointBase>(_connection))
                {
                    if (point is FloutecMeasureLine)
                    {
                        var line = repo.GetAll()
                            .Where(e => e.Id == point.Id)
                            .OfType<FloutecMeasureLine>()
                            .Include(l => l.AlarmData)
                            .Include(l => l.HourlyData)
                            .Include(l => l.IdentData)
                            .Include(l => l.InstantData)
                            .Include(l => l.InterData)
                            .Single();

                        repo.Delete(new List<FloutecMeasureLine> { line });
                    }

                    if (point is Roc809MeasurePoint)
                    {
                        var line = repo.GetAll()
                            .Where(e => e.Id == point.Id)
                            .OfType<Roc809MeasurePoint>()
                            .Include(l => l.DailyData)
                            .Include(l => l.MinuteData)
                            .Include(l => l.PeriodicData)
                            .Single();

                        repo.Delete(new List<Roc809MeasurePoint> { line });
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