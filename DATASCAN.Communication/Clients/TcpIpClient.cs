using System.Net.Sockets;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Communication.Clients
{
    /// <summary>
    /// Клиент для соединения с вычислителем ROC809
    /// </summary>
    public class TcpIpClient : IClient
    {
        public async Task<byte[]> GetData(Roc809 roc, byte[] request)
        {
            var client = new TcpClient();

            // Если соединение не было установлено, то установить
            if (!client.Connected)
                client.Connect(roc.Address, roc.Port);

            // Получение потока
            var stream = client.GetStream();

            // Запись запроса в поток
            await stream.WriteAsync(request, 0, request.Length);
            await stream.FlushAsync();

            // Чтение ответа из потока
            var response = new byte[1024];
            await stream.ReadAsync(response, 0, response.Length);

            return response;          
        }
    }
}
