using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model.Scanning;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным групп опроса
    /// </summary>
    public class ScansService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным групп опроса
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public ScansService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию групп опроса
        /// </summary>
        /// <returns>Коллекция групп опроса</returns>
        public async Task<List<ScanBase>> GetAll(Action<Exception> onException = null)
        {
            List<ScanBase> scans = new List<ScanBase>();

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<ScanBase> repo = new EntityRepository<ScanBase>(_connection))
                {
                    scans = repo.GetAll().Include(s => s.Members).ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return scans;
        }
    }
}