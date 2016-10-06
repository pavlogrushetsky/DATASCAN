using DATASCAN.Model.Scanning;

namespace DATASCAN.Services
{
    public class ScanMembersService : EntitiesService<ScanMemberBase>
    {
        public ScanMembersService(string connection) : base(connection)
        {
        }
    }
}