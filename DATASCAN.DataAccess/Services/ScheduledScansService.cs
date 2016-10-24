using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.DataAccess.Repositories;

namespace DATASCAN.DataAccess.Services
{
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class ScheduledScansService : EntitiesService<ScheduledScan>
    {
        public ScheduledScansService(string connection) : base(connection)
        {
        }

        public override async Task Update(ScheduledScan scan, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var scansRepo = new EntityRepository<ScheduledScan>(_connection))
                {
                    var existingScan = scansRepo.GetAll().Where(s => s.Id == scan.Id).Include(s => s.Periods).Single();

                    var periodsToDelete = existingScan.Periods.Where(ep => !scan.Periods.Select(p => p.Id).Contains(ep.Id)).ToList();
                    var periodsToAdd = scan.Periods.Where(p => p.Scan == null).ToList();

                    if (periodsToDelete.Any())
                    {
                        using (var periodsRepo = new EntityRepository<ScanPeriod>(_connection))
                        {
                            periodsToDelete.ForEach(s =>
                            {
                                periodsRepo.Delete(s.Id);
                            });
                        }
                    }                    

                    periodsToAdd.ForEach(s =>
                    {
                        s.ScanId = existingScan.Id;
                        s.Scan = existingScan;
                        existingScan.Periods.Add(s);
                    });

                    existingScan.DateModified = DateTime.Now;
                    existingScan.Title = scan.Title;
                    existingScan.IsActive = scan.IsActive;
                    scansRepo.Update(existingScan);
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

        public async Task Delete(ScheduledScan scan, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var repo = new EntityRepository<ScheduledScan>(_connection))
                {
                    var sc = repo.GetAll()
                        .Where(s => s.Id == scan.Id)
                        .Include(s => s.Periods)
                        .Include(s => s.Members)
                        .Single();
                    repo.Delete(new List<ScheduledScan> { sc });
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