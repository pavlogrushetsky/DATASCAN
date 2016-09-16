using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным заказчиков
    /// </summary>
    public class CustomersService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным заказчиков
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public CustomersService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию заказчиков
        /// </summary>
        /// <returns>Коллекция заказчиков</returns>
        public async Task<List<Customer>> GetAll(Action<Exception> onException = null)
        {
            List<Customer> customers = new List<Customer>();

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<Customer> repo = new EntityRepository<Customer>(_connection))
                {
                    customers = repo.GetAll().OrderBy(o => o.Title).ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return customers;
        }
    }
}