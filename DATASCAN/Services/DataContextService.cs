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
        public async Task<bool> TestConnection(Action<Exception> onException = null)
        {
            try
            {
                DbConnection connection = new SqlConnection(_connection);

                using (DataContext context = new DataContext(connection))
                {
                    await context.Database.Connection.OpenAsync();
                }
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
                return false;
            }

            return true;
        }
    }
}
