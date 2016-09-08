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
        /// <summary>
        /// Возвращает true, если соединение установлено, иначе false
        /// </summary>
        /// <param name="connectionString">Строка соединения</param>
        /// <returns></returns>
        public async Task<bool> TestConnection(string connectionString)
        {
            try
            {
                DbConnection connection = new SqlConnection(connectionString);

                using (DataContext context = new DataContext(connection))
                {
                    await context.Database.Connection.OpenAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
