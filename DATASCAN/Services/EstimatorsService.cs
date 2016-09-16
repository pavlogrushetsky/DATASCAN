using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным вычислителей
    /// </summary>
    public class EstimatorsService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным вычислителей
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public EstimatorsService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию вычислителей
        /// </summary>
        /// <returns>Коллекция вычислителей</returns>
        public async Task<List<EstimatorBase>> GetAll(Action<Exception> onException = null)
        {
            List<EstimatorBase> estimators = new List<EstimatorBase>();

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<EstimatorBase> repo = new EntityRepository<EstimatorBase>(_connection))
                {
                    estimators = repo.GetAll()
                        .Include(c => c.Customer)
                        .Include(c => c.Group)
                        .Include(c => c.MeasurePoints)
                        .OrderBy(o => o.Id)
                        .ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return estimators;
        }
    }
}