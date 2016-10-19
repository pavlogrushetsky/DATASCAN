using System.Collections.Generic;
using DATASCAN.Model.Scanning;

namespace DATASCAN.Connection.Services
{
    public abstract class ConnectionServiceBase
    {
        public abstract void Process(IEnumerable<ScanMemberBase> members);
    }
}