using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Model.Rocs;
using DATASCAN.Repositories;

namespace DATASCAN.Services
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
                using (EntityRepository<EstimatorBase> repo = new EntityRepository<EstimatorBase>(_connection))
                {
                    if (estimator is Roc809)
                    {
                        Roc809 roc = repo.GetAll().Where(e => e.Id == estimator.Id).OfType<Roc809>().Include(e => e.AlarmData).Include(e => e.EventData).Include(e => e.MeasurePoints).Single();
                        repo.Delete(new List<Roc809> { roc });
                    }
                    else
                    {
                        EstimatorBase est = repo.GetAll().Where(e => e.Id == estimator.Id).Include(e => e.MeasurePoints).Single();
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