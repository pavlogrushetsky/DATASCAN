using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
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
                using (var repo = new EntityRepository<PeriodicScan>(_connection))
                {
                    var sc = repo.GetAll()
                        .Where(s => s.Id == scanId)
                        .Include(s => s.Members)
                        .Single();
                    repo.Delete(new List<PeriodicScan> { sc });
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