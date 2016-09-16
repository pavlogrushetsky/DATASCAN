using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным групп вычислителей
    /// </summary>
    public class EstimatorsGroupsService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным групп вычислителей
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public EstimatorsGroupsService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию групп вычислителей
        /// </summary>
        /// <returns>Коллекция групп вычислителей</returns>
        public async Task<List<EstimatorsGroup>> GetAll(Action<Exception> onException = null)
        {
            List<EstimatorsGroup> groups = new List<EstimatorsGroup>();

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<EstimatorsGroup> repo = new EntityRepository<EstimatorsGroup>(_connection))
                {
                    groups = repo.GetAll().OrderBy(o => o.Name).ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return groups;
        }
    }
}