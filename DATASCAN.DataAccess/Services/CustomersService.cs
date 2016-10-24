using System;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    public class CustomersService : EntitiesService<Customer>
    {
        public CustomersService(string connection) : base(connection)
        {
        }

        public async Task Delete(int customerId, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<Customer>(_connection))
                {
                    var customer = repo.Get(customerId);
                    customer.Estimators.ToList().ForEach(e => e.Customer = null);
                    customer.Groups.ToList().ForEach(g => g.Customer = null);
                    repo.Delete(customerId);
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