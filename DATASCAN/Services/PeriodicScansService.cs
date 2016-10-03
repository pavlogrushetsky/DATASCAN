using DATASCAN.Model.Scanning;

namespace DATASCAN.Services
{
    public class PeriodicScansService : EntitiesService<PeriodicScan>
    {
        public PeriodicScansService(string connection) : base(connection)
        {
        }
    }
}