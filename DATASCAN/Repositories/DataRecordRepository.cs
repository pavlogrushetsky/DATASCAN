using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DATASCAN.Context;
using DATASCAN.Model.Common;

namespace DATASCAN.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к данным опросов
    /// </summary>
    /// <typeparam name="DataRecord"><see cref="DataRecordBase"/>Базовый абстрактный класс</typeparam>
    public class DataRecordRepository<DataRecord> : IDisposable where DataRecord : DataRecordBase
    {
        // Контекст данных
        private readonly DataContext _context;

        private bool _disposed;

        /// <summary>
        /// Создание репозитория для доступа к данным опросов на основе строки подключения
        /// </summary>
        /// <param name="connectionString"></param>
        public DataRecordRepository(string connectionString)
        {
            // Создание соединения и инициализация контекста данных
            SqlConnection connection = new SqlConnection(connectionString);
            _context = new DataContext(connection, false);
        }

        /// <summary>
        /// Возвращает все данные указанного типа
        /// </summary>
        public IQueryable<DataRecord> GetAll()
        {
            return _context.Set<DataRecord>();
        }

        /// <summary>
        /// Возвращает данные указанного типа по уникальному идентификатору
        /// </summary>
        public DataRecord Get(int id)
        {
            return _context.Set<DataRecord>().SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Добавляет коллекцию данных указанного типа
        /// </summary>
        public void Insert(IEnumerable<DataRecord> records)
        {
            _context.Set<DataRecord>().AddRange(records);
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавляет данные указанного типа
        /// </summary>
        public void Insert(DataRecord record)
        {
            _context.Set<DataRecord>().Add(record);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет все данные указанного типа
        /// </summary>
        public void DeleteAll()
        {
            _context.Set<DataRecord>().RemoveRange(GetAll());
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет коллекцию данных указанного типа
        /// </summary>
        public void Delete(IEnumerable<DataRecord> records)
        {
            _context.Set<DataRecord>().RemoveRange(records);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет данные указанного типа по уникальному идентификатору
        /// </summary>
        public void Delete(int id)
        {
            _context.Set<DataRecord>().Remove(Get(id));
            _context.SaveChanges();
        }

        /*
        Реализация интерфейса IDisposable
        */
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
