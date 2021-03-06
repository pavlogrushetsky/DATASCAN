﻿using System;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    public class ScanMembersService : EntitiesService<ScanMemberBase>
    {
        public ScanMembersService(string connection) : base(connection)
        {
        }

        public async Task Delete(int scanId, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<ScanMemberBase>(_connection))
                {
                    repo.Delete(scanId);
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
                    onSuccess?.Invoke();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}