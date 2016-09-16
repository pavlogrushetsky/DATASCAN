using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным точек измерения
    /// </summary>
    public class MeasurePointsService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным точек измерения
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public MeasurePointsService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию точек измерения
        /// </summary>
        /// <returns>Коллекция точек измерения</returns>
        public async Task<List<MeasurePointBase>> GetAll(Action<Exception> onException = null)
        {
            List<MeasurePointBase> points = new List<MeasurePointBase>();

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<MeasurePointBase> repo = new EntityRepository<MeasurePointBase>(_connection))
                {
                    points = repo.GetAll().OrderBy(o => o.Id).ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return points;
        }
    }
}