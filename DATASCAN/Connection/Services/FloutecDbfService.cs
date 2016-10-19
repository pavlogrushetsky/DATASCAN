using System.Collections.Generic;
using DATASCAN.Model;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Services
{
    public class FloutecDbfService : ConnectionServiceBase
    {
        private string _dbfConnection;

        public FloutecDbfService(LogListView log) : base(log)
        {
            
        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _dbfConnection = Infrastructure.Settings.Settings.DbfPath;
        }
    }
}