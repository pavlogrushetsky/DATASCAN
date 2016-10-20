using System.Collections.Generic;
using System.Linq;
using DATASCAN.Model;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Scanners
{
    public class RocScanner : ScannerBase
    {
        public RocScanner(LogListView log) : base(log)
        {

        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {
            _connection = connection;
            _members = members.ToList();
            _estimators = estimators.ToList();
        }
    }
}