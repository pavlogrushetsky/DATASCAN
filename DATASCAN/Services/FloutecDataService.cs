using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Model.Floutecs;
using DATASCAN.Repositories;
using DATASCAN.Repositories.Extensions;

namespace DATASCAN.Services
{
    public class FloutecDataService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным идентификации вычислителей ФЛОУТЭК
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public FloutecDataService(string connection)
        {
            _connection = connection;
        }

        public async Task SaveIdentData(FloutecIdentData data, Action<bool> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<FloutecIdentData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.FloutecMeasureLineId == data.FloutecMeasureLineId)
                        .OrderByDescending(o => o.DateAdded)
                        .FirstOrDefault();

                    if (lastData != null && lastData.IsEqual(data))
                        return false;

                    repo.Insert(data);

                    return true;
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
                    onSuccess?.Invoke(result.Result);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async Task SaveInterData(int lineId, List<FloutecInterData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<FloutecInterData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.FloutecMeasureLineId == lineId)
                        .OrderByDescending(o => o.DAT)
                        .FirstOrDefault();

                    if (lastData != null)
                    {
                        var filtered = data.Where(d => d.DAT > lastData.DAT).ToList();
                        repo.Insert(filtered);
                        return filtered.Count;
                    }

                    repo.Insert(data);

                    return data.Count;
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
                    onSuccess?.Invoke(result.Result);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}