﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Floutecs;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.Services
{
    public class FloutecService
    {
        private readonly string _connection;

        public FloutecService(string connection)
        {
            _connection = connection;
        }

        public async Task GetIdentData(int address, int number, Action<FloutecIdentData> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DbfRepository(_connection))
                {
                    return repo.GetIdentData(address, number);
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

        public async Task GetInterData(int address, int number, Action<List<FloutecInterData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DbfRepository(_connection))
                {
                    return repo.GetAllInterData(address, number);
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

        public async Task GetAlarmData(int address, int number, Action<List<FloutecAlarmData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DbfRepository(_connection))
                {
                    return repo.GetAllAlarmData(address, number);
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

        public async Task GetHourlyData(int address, int number, Action<List<FloutecHourlyData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DbfRepository(_connection))
                {
                    return repo.GetAllHourlyData(address, number);
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

        public async Task GetInstantData(int address, int number, Action<FloutecInstantData> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new DbfRepository(_connection))
                {
                    return repo.GetInstantData(address, number);
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