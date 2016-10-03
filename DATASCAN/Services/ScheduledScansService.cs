using System;
using System.Threading.Tasks;
using DATASCAN.Model.Scanning;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    public class ScheduledScansService : EntitiesService<ScheduledScan>
    {
        public ScheduledScansService(string connection) : base(connection)
        {
        }

        public override async Task Update(ScheduledScan scan, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<ScheduledScan> repo = new EntityRepository<ScheduledScan>(_connection))
                {
                    scan.DateModified = DateTime.Now;
                    repo.Update(scan);
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