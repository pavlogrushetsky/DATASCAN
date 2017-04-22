using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    public class RocDataService
    {
        private readonly string _connection;

        public RocDataService(string connection)
        {
            _connection = connection;
        }

        public async Task SaveMinuteData(int pointId, List<Roc809MinuteData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<Roc809MinuteData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.Roc809MeasurePointId == pointId)
                        .OrderByDescending(o => o.Period)
                        .FirstOrDefault();

                    if (lastData != null)
                    {
                        var filtered = data.Where(d => d.Period > lastData.Period).ToList();
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

        public async Task SavePeriodicData(int pointId, List<Roc809PeriodicData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<Roc809PeriodicData>(_connection))
                {
                    var existent = repo.GetAll().Where(d => d.Roc809MeasurePointId == pointId).Select(d => d.Period);
                    var filtered = data.Where(d => !existent.Contains(d.Period)).ToList();

                    if (!filtered.Any())
                    {
                        return 0;
                    }

                    repo.Insert(filtered);
                    return filtered.Count;
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

        public async Task SaveDailyData(int pointId, List<Roc809DailyData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<Roc809DailyData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.Roc809MeasurePointId == pointId)
                        .OrderByDescending(o => o.Period)
                        .FirstOrDefault();

                    if (lastData != null)
                    {
                        var filtered = data.Where(d => d.Period > lastData.Period).ToList();
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

        public async Task SaveEventData(int rocId, List<Roc809EventData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<Roc809EventData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.Roc809Id == rocId)
                        .OrderByDescending(o => o.Time)
                        .FirstOrDefault();

                    if (lastData != null)
                    {
                        var filtered = data.Where(d => d.Time > lastData.Time).ToList();
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

        public async Task SaveAlarmData(int rocId, List<Roc809AlarmData> data, Action<int> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DataRecordRepository<Roc809AlarmData>(_connection))
                {
                    var lastData = repo.GetAll()
                        .Where(d => d.Roc809Id == rocId)
                        .OrderByDescending(o => o.Time)
                        .FirstOrDefault();

                    if (lastData != null)
                    {
                        var filtered = data.Where(d => d.Time > lastData.Time).ToList();
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