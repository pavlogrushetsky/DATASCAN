using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities;
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
                {
                    if (estimator is Roc809)
                    {
                        var roc = repo.GetAll().Where(e => e.Id == estimator.Id).OfType<Roc809>().Include(e => e.AlarmData).Include(e => e.EventData).Include(e => e.MeasurePoints).Include(e => e.Scans).Single();
                        repo.Delete(new List<Roc809> { roc });
                    }
                    else
                    {
                        var est = repo.GetAll().Where(e => e.Id == estimator.Id).Include(e => e.MeasurePoints).Include(e => e.Scans).Single();
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