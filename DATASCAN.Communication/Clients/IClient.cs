using System.Threading.Tasks;

namespace DATASCAN.Communication.Clients
{
    public interface IClient
    {
        Task<byte[]> GetData(byte[] request);
    }
}