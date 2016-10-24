using System.Collections.Generic;
using System.Threading.Tasks;
using DATASCAN.Core.Model;
using DATASCAN.Core.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Scanners
{
    public abstract class ScannerBase
    {
        protected readonly LogListView _log;
        protected readonly TaskScheduler _uiSyncContext ;

        protected string _connection;
        protected List<ScanMemberBase> _members;
        protected List<EstimatorBase> _estimators;

        protected ScannerBase(LogListView log)
        {
            _log = log;
            _uiSyncContext = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public abstract void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators);
    }
}