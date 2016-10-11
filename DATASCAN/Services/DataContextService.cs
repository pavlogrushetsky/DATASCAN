using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DATASCAN.Context;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис доступа к данным контекста данных
    /// </summary>
    public class DataContextService
    {
        private readonly string _connection;

        /// <summary>
        /// Сервис доступа к данным контекста данных
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public DataContextService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает true, если соединение установлено, иначе false
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TestConnection(bool initialize, Action<Exception> onException = null)
        {
            return await Task.Factory.StartNew(() =>
            {
                DbConnection connection = new SqlConnection(_connection);

                using (DataContext context = new DataContext(connection, initialize))
                {
                    context.Database.Connection.Open();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                    return false;
                }

                return true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
