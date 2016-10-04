using System;
using System.Threading.Tasks;
using DATASCAN.Model.Scanning;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    public class PeriodicScansService : EntitiesService<PeriodicScan>
    {
        public PeriodicScansService(string connection) : base(connection)
        {
        }

        public async Task Delete(int scanId, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<PeriodicScan> repo = new EntityRepository<PeriodicScan>(_connection))
                {
                    repo.Delete(scanId);
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