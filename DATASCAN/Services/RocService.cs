using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Communication.Clients;
using DATASCAN.Communication.Protocols;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Services
{
    public class RocService
    {
        private readonly RocPlusProtocol _rocPlusProtocol;

        public RocService()
        {
            _rocPlusProtocol = new RocPlusProtocol();
        }

        public async Task GetEventData(IClient client, Roc809 roc, Action<List<Roc809EventData>> onSuccess, Action<Exception> onException)
        {
            await _rocPlusProtocol.GetEventData(roc, client)
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

        public async Task GetAlarmData(IClient client, Roc809 roc, Action<List<Roc809AlarmData>> onSuccess, Action<Exception> onException)
        {
            await _rocPlusProtocol.GetAlarmData(roc, client)
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

        public async Task GetMinuteData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809MinuteData>> onSuccess, Action<Exception> onException)
        {
            await _rocPlusProtocol.GetPeriodicData(roc, point, RocHistoryType.Minute, client)
            .ContinueWith(result =>
            {
                return result.Result.Select(d => new Roc809MinuteData { Period = d.DatePeriod, Value = d.Value }).ToList();
            })        
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

        public async Task GetPeriodicData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809PeriodicData>> onSuccess, Action<Exception> onException)
        {
            await _rocPlusProtocol.GetPeriodicData(roc, point, RocHistoryType.Periodic, client)
            .ContinueWith(result => 
            {
                return result.Result.Select(d => new Roc809PeriodicData { Period = d.DatePeriod, Value = d.Value }).ToList();
            })                
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

        public async Task GetDailyData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809DailyData>> onSuccess, Action<Exception> onException)
        {
            await _rocPlusProtocol.GetPeriodicData(roc, point, RocHistoryType.Daily, client)
            .ContinueWith(result =>
            {
                return result.Result.Select(d => new Roc809DailyData { Period = d.DatePeriod, Value = d.Value }).ToList();
            })                
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