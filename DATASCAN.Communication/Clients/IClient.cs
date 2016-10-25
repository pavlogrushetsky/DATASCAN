namespace DATASCAN.Communication.Clients
{
    public interface IClient
    {
        byte[] GetData(byte[] request);
    }
}