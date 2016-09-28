using System;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
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
                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_connection))
                {
                    Customer customer = repo.Get(customerId);
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