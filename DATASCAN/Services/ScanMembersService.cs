using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    public class ScanMembersService : EntitiesService<ScanMemberBase>
    {
        public ScanMembersService(string connection) : base(connection)
        {
        }

        public async Task Delete(int scanId, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<ScanMemberBase> repo = new EntityRepository<ScanMemberBase>(_connection))
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