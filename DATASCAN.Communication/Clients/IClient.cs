using System.Threading.Tasks;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Communication.Clients
{
    public interface IClient
    {
        Task<byte[]> GetData(Roc809 roc, byte[] request);
    }
}