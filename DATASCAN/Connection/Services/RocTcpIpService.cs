using System.Collections.Generic;
using DATASCAN.Model;
using DATASCAN.Model.Scanning;
using DATASCAN.View.Controls;

namespace DATASCAN.Connection.Services
{
    public class RocTcpIpService : ConnectionServiceBase
    {
        public RocTcpIpService(LogListView log) : base(log)
        {

        }

        public override void Process(string connection, IEnumerable<ScanMemberBase> members, IEnumerable<EstimatorBase> estimators)
        {

        }
    }
}