using System;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    public class GroupsService : EntitiesService<EstimatorsGroup>
    {
        public GroupsService(string connection) : base(connection)
        {
        }

        public async Task Delete(int groupId, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<EstimatorsGroup>(_connection))
                {
                    var group = repo.Get(groupId);
                    group.Estimators.ToList().ForEach(e => e.Group = null);
                    repo.Delete(groupId);
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