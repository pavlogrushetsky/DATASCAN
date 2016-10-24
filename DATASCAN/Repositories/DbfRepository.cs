using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using DATASCAN.Core.Model.Floutecs;
using DATASCAN.Repositories.Extensions;

namespace DATASCAN.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к данным таблиц Dbf
    /// </summary>
    public class DbfRepository : IDisposable
    {
        #region Конструктор и поля

        // Признак освобождения ресурсов
        private bool _disposed;

        // Контекст доступа к данным таблиц Dbf (соединение)
        private readonly OleDbConnection _connection;

        // Команда доступа к данным таблиц Dbf
        private readonly OleDbCommand _command;

        /// <summary>
        /// Конструктор репозитория доступа к данным таблиц Dbf
        /// <param name="path">Путь к таблицам Dbf</param>
        /// </summary>
        public DbfRepository(string path)
        {
            // Формирование строки соединения
            string connectionString = $@"Data Source={path};Provider=VFPOLEDB.1;Collating Sequence=MACHINE;";

            // Инициализация контекста доступа к данным (соединения)
            _connection = new OleDbConnection(connectionString);

            // Создание команды
            _command = _connection.CreateCommand();

            // Открытие соединения
            _connection.Open();
        }

        #endregion

        #region Операции доступа к статическим данным и данным идентификации

        /// <summary>
        /// Получение данных идентификации
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <returns>Данные идентификации</returns>
        public FloutecIdentData GetIdentData(int address, int line)
        {
            // Создание объекта данных идентификации
            FloutecIdentData identData = new FloutecIdentData();
            int n_flonit = address * 10 + line;
            identData.N_FLONIT = n_flonit;

            bool hasData = false;

            // Формирование строки соединения с таблицей данных идентификации
            _command.CommandText = "SELECT DISTINCT * FROM ident.DBF WHERE N_FLONIT=" + n_flonit;

            // Чтение данных идентификации
            using (OleDbDataReader reader = _command.ExecuteReader())
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        identData.FromIdentTable(reader);
                    }

                    hasData = reader.HasRows;
                }
            }

            // Формирование строки соединения с таблицей статических данных
            _command.CommandText = "SELECT DISTINCT * FROM stat.DBF WHERE N_FLONIT=" + n_flonit;

            // Чтение статических данных
            using (OleDbDataReader reader = _command.ExecuteReader())
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        identData.FromStatTable(reader);
                    }

                    hasData = hasData || reader.HasRows;
                }
            }

            if (hasData)
                return identData;
            return null;
        }

        #endregion

        #region Операции доступа к часовым данным

        /// <summary>
        /// Получение часовых данных за указанный период
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <param name="from">Начало периода</param>
        /// <param name="to">Конец периода</param>
        /// <returns>Часовые данные за указанный период</returns>
        public List<FloutecHourlyData> GetHourlyData(int address, int line, DateTime from, DateTime to)
        {
            // Инициализация пустой коллекции часовых данных
            List<FloutecHourlyData> hourlyData = new List<FloutecHourlyData>();

            // Если период указан верно, то ...
            if (to >= from)
            {                
                int n_flonit = address * 10 + line;

                // Формирование запроса
                _command.CommandText = "SELECT DISTINCT * FROM rour.DBF WHERE N_FLONIT=" + n_flonit;

                try
                {
                    // Выполнение команды
                    using (OleDbDataReader reader = _command.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            hourlyData.FromHourTable(reader);
                        }
                    }
                }
                catch(Exception)
                {
                    _command.CommandText = "SELECT DISTINCT * FROM rou45.DBF WHERE N_FLONIT=" + n_flonit;

                    using (OleDbDataReader reader = _command.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            hourlyData.FromHourTable(reader);
                        }
                    }
                }

                hourlyData.ForEach(h => h.N_FLONIT = n_flonit);
            }

            return hourlyData.Where(h => h.DAT > from && h.DAT <= to).ToList();
        }

        /// <summary>
        /// Получение всех часовых данных
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <returns>Все часовые данные</returns>
        public List<FloutecHourlyData> GetAllHourlyData(int address, int line)
        {
            // Инициализация пустой коллекции часовых данных
            List<FloutecHourlyData> hourlyData = new List<FloutecHourlyData>();

            int n_flonit = address * 10 + line;

            // Формирование запроса
            _command.CommandText = "SELECT DISTINCT * FROM rour.DBF WHERE N_FLONIT=" + n_flonit;

            try
            {
                // Выполнение команды
                using (OleDbDataReader reader = _command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        hourlyData.FromHourTable(reader);
                    }
                }
            }
            catch(Exception)
            {
                _command.CommandText = "SELECT DISTINCT * FROM rou45.DBF WHERE N_FLONIT=" + n_flonit;

                using (OleDbDataReader reader = _command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        hourlyData.FromHourTable(reader);
                    }
                }
            }

            hourlyData.ForEach(h => h.N_FLONIT = n_flonit);

            return hourlyData;
        }

        #endregion

        #region Операции доступа к мгновенным данным

        /// <summary>
        /// Получение данных идентификации
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <returns>Мгновенные данные</returns>
        public FloutecInstantData GetInstantData(int address, int line)
        {
            // Создание объекта мгновенных данных
            FloutecInstantData instantData = new FloutecInstantData();
            int n_flonit = address * 10 + line;
            instantData.N_FLONIT = n_flonit;

            // Формирование строки соединения с таблицей мгновенных
            _command.CommandText = "SELECT DISTINCT * FROM mgnov.DBF WHERE N_FLONIT=" + n_flonit;

            // Чтение мгновенных
            using (OleDbDataReader reader = _command.ExecuteReader())
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        instantData.FromInstTable(reader);
                    }

                    if (!reader.HasRows)
                        return null;
                }
            }

            return instantData;
        }

        #endregion

        #region Операции доступа к данным аварий

        /// <summary>
        /// Получение данных аварий за указанный период
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <param name="from">Начало периода</param>
        /// <param name="to">Конец периода</param>
        /// <returns>Данные аварий</returns>
        public List<FloutecAlarmData> GetAlarmData(int address, int line, DateTime from, DateTime to)
        {
            // Инициализация пустой коллекции данных аварий
            List<FloutecAlarmData> alarmData = new List<FloutecAlarmData>();

            // Если период указан верно, то ...
            if (to >= from)
            {
                int n_flonit = address * 10 + line;

                // Формирование запроса
                _command.CommandText = "SELECT DISTINCT * FROM avar.DBF WHERE N_FLONIT=" + n_flonit;

                // Выполнение команды
                using (OleDbDataReader reader = _command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        alarmData.FromAvarTable(reader);
                    }
                }

                alarmData.ForEach(h => h.N_FLONIT = n_flonit);
            }

            return alarmData.Where(h => h.DAT > from && h.DAT <= to).ToList();
        }

        /// <summary>
        /// Получение всех данных аварий
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <returns>Все данные аварий</returns>
        public List<FloutecAlarmData> GetAllAlarmData(int address, int line)
        {
            // Инициализация пустой коллекции данных
            List<FloutecAlarmData> alarmData = new List<FloutecAlarmData>();

            int n_flonit = address * 10 + line;

            // Формирование запроса
            _command.CommandText = "SELECT DISTINCT * FROM avar.DBF WHERE N_FLONIT=" + n_flonit;

            // Выполнение команды
            using (OleDbDataReader reader = _command.ExecuteReader())
            {
                while (reader != null && reader.Read())
                {
                    alarmData.FromAvarTable(reader);
                }
            }

            alarmData.ForEach(h => h.N_FLONIT = n_flonit);

            return alarmData;
        }

        #endregion

        #region Операции доступа к данным вмешательств

        /// <summary>
        /// Получение всех данных вмешательств
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <returns>Все данные вмешательств</returns>
        public List<FloutecInterData> GetAllInterData(int address, int line)
        {
            // Инициализация пустой коллекции данных
            List<FloutecInterData> interData = new List<FloutecInterData>();

            int n_flonit = address * 10 + line;

            // Формирование запроса
            _command.CommandText = "SELECT DISTINCT * FROM vmesh.DBF WHERE N_FLONIT=" + n_flonit;

            // Выполнение команды
            using (OleDbDataReader reader = _command.ExecuteReader())
            {
                while (reader != null && reader.Read())
                {
                    interData.FromVmeshTable(reader);
                }
            }

            interData.ForEach(h => h.N_FLONIT = n_flonit);

            return interData;
        }

        /// <summary>
        /// Получение данных вмешательств за указанный период
        /// </summary>
        /// <param name="address">Адрес вычислителя</param>
        /// <param name="line">Номер нитки</param>
        /// <param name="from">Начало периода</param>
        /// <param name="to">Конец периода</param>
        /// <returns>Данные вмешательств</returns>
        public List<FloutecInterData> GetInterData(int address, int line, DateTime from, DateTime to)
        {
            // Инициализация пустой коллекции данных вмешательств
            List<FloutecInterData> interData = new List<FloutecInterData>();

            // Если период указан верно, то ...
            if (to >= from)
            {
                int n_flonit = address * 10 + line;

                // Формирование запроса
                _command.CommandText = "SELECT DISTINCT * FROM vmesh.DBF WHERE N_FLONIT=" + n_flonit;

                // Выполнение команды
                using (OleDbDataReader reader = _command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        interData.FromVmeshTable(reader);
                    }
                }

                interData.ForEach(h => h.N_FLONIT = n_flonit);
            }

            return interData.Where(h => h.DAT > from && h.DAT <= to).ToList();
        }


        #endregion

        #region Освобождение ресурсов

        /*
            Реализация интерфейса IDisposable для автоматического освобождения ресурсов
            контекста доступа к данным при использовании конструкции using
        */

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _command.Cancel();
                    _command.Dispose();

                    if (_connection.State != System.Data.ConnectionState.Closed)
                        _connection.Close();

                    _connection.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
