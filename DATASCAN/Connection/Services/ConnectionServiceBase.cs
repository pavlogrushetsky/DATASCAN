using System.Collections.Generic;
using System.Threading.Tasks;
using DATASCAN.Model;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Services
{
    public abstract class ConnectionServiceBase
    {
        private readonly LogListView _log;
        private readonly TaskScheduler _uiSyncContext ;

        protected ConnectionServiceBase(LogListView log)
        {
            _log = log;
            _uiSyncContext = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public abstract void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators);
    }
}