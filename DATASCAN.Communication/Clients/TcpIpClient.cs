using System.Net.Sockets;
using System.Threading.Tasks;

namespace DATASCAN.Communication.Clients
{
    /// <summary>
    /// Клиент для соединения с вычислителем ROC809
    /// </summary>
    public class TcpIpClient : IClient
    {
        #region Конструктор и поля

        private readonly TcpClient _client;
        private NetworkStream stream;

        private readonly string _ip;
        private readonly int _port;

        /// <summary>
        /// Клиент для соединения с вычислителем ROC809
        /// </summary>
        /// <param name="ip">Ip-адрес</param>
        /// <param name="port">Порт</param>
        public TcpIpClient(string ip, int port)
        {
            // Инициализация полей
            _ip = ip;
            _port = port;
            _client = new TcpClient();
        }

        #endregion

        /// <summary>
        /// Получение данных из вычислителя ROC809
        /// </summary>
        /// <param name="request">Массив байтов запроса</param>
        /// <returns>Массив байтов ответа</returns>
        public Task<byte[]> GetData(byte[] request)
        {
            return Task.Factory.StartNew(() =>
            {
                // Если соединение не было установлено, то установить
                if (!_client.Connected)
                    _client.Connect(_ip, _port);

                // Получение потока
                stream = _client.GetStream();

                // Запись запроса в поток
                stream.Write(request, 0, request.Length);
                stream.Flush();

                // Чтение ответа из потока
                var response = new byte[1024];
                stream.Read(response, 0, response.Length);

                return response;
            });           
        }
    }
}
