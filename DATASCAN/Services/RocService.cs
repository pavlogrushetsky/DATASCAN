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
        public async Task GetEventData(IClient client, Roc809 roc, Action<List<Roc809EventData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                var protocol = new RocPlusProtocol();
                return protocol.GetEventData(roc, client);
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

        public async Task GetAlarmData(IClient client, Roc809 roc, Action<List<Roc809AlarmData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                var protocol = new RocPlusProtocol();
                return protocol.GetAlarmData(roc, client);
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

        public async Task GetMinuteData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809MinuteData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                var protocol = new RocPlusProtocol();
                var data = protocol.GetPeriodicData(roc, point, RocHistoryType.Minute, client);
                return data.Select(d => new Roc809MinuteData {Period = d.DatePeriod, Value = d.Value}).ToList();
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

        public async Task GetPeriodicData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809PeriodicData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                var protocol = new RocPlusProtocol();
                var data = protocol.GetPeriodicData(roc, point, RocHistoryType.Periodic, client);
                return data.Select(d => new Roc809PeriodicData { Period = d.DatePeriod, Value = d.Value }).ToList();
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

        public async Task GetDailyData(IClient client, Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809DailyData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                var protocol = new RocPlusProtocol();
                var data = protocol.GetPeriodicData(roc, point, RocHistoryType.Daily, client);
                return data.Select(d => new Roc809DailyData { Period = d.DatePeriod, Value = d.Value }).ToList();
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