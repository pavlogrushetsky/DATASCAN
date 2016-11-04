using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DATASCAN.Communication.Clients;
using DATASCAN.Communication.Protocols;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Services
{
    public class RocService
    {
        public async Task GetPort(List<string> ports, string phone, int baudRate, Parity parity, int dataBits, StopBits stopBits, Action<string> onSuccess, Action<Exception> onException)
        {
            await Task.Run(async () =>
            {
                var port = "";

                if (!ports.Any())
                    return port;

                var client = new GprsClient(phone, ports[0], baudRate, parity, dataBits, stopBits);
                var retries = 3;
                do
                {
                    var status = await client.TestConnection();
                    if (status.Contains("CONNECT"))
                    {
                        port = ports[0];
                        break;
                    }
                    await Task.Delay(1000);
                    retries--;
                } while (retries > 0);

                if (ports.Count <= 1)
                    return port;

                client = new GprsClient(phone, ports[1], baudRate, parity, dataBits, stopBits);
                retries = 3;
                do
                {
                    var status = await client.TestConnection();
                    if (status.Contains("CONNECT"))
                    {
                        port = ports[1];
                        break;
                    }
                    await Task.Delay(1000);
                    retries--;
                } while (retries > 0);

                if (ports.Count <= 2)
                    return port;

                client = new GprsClient(phone, ports[2], baudRate, parity, dataBits, stopBits);
                retries = 3;
                do
                {
                    var status = await client.TestConnection();
                    if (status.Contains("CONNECT"))
                    {
                        port = ports[2];
                        break;
                    }
                    await Task.Delay(1000);
                    retries--;
                } while (retries > 0);

                return port;
            }).ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke(result.Result);
                }
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());           
        }

        public async Task GetEventData(IClient client, Roc809 roc, Action<List<Roc809EventData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(async () =>
            {
                var protocol = new RocPlusProtocol();
                return await protocol.GetEventData(roc, client);
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke(result.Result.Result);
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