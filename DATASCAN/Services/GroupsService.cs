using System;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
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
                using (EntityRepository<EstimatorsGroup> repo = new EntityRepository<EstimatorsGroup>(_connection))
                {
                    EstimatorsGroup group = repo.Get(groupId);
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