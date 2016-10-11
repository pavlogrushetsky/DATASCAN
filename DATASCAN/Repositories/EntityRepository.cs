using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DATASCAN.Context;
using DATASCAN.Model.Common;

namespace DATASCAN.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к данным сущностей
    /// </summary>
    /// <typeparam name="Entity"><see cref="EntityBase"/>Базовый абстрактный класс</typeparam>
    public class EntityRepository<Entity> : IDisposable where Entity : EntityBase
    {
        // Контекст данных
        private readonly DataContext _context;

        private bool _disposed;

        /// <summary>
        /// Создание репозитория для доступа к данным сущностей на основе строки подключения
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityRepository(string connectionString)
        {
            // Создание соединения и инициализация контекста данных
            SqlConnection connection = new SqlConnection(connectionString);
            _context = new DataContext(connection, false);
        }

        /// <summary>
        /// Возвращает все сущности указанного типа
        /// </summary>
        public IQueryable<Entity> GetAll()
        {
            return _context.Set<Entity>();
        }

        /// <summary>
        /// Возвращает сущность указанного типа по уникальному идентификатору
        /// </summary>
        public Entity Get(int id)
        {
            return _context.Set<Entity>().SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Добавляет коллекцию сущностей указанного типа
        /// </summary>
        public void Insert(IEnumerable<Entity> entities)
        {
            _context.Set<Entity>().AddRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавляет сущность указанного типа
        /// </summary>
        public void Insert(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Обновляет данные коллекции сущностей указанного типа
        /// </summary>
        public void Update(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Обновляет данные сущности указанного типа
        /// </summary>
        public void Update(Entity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет все сущности указанного типа
        /// </summary>
        public void DeleteAll()
        {
            _context.Set<Entity>().RemoveRange(GetAll());
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет коллекцию сущностей указанного типа
        /// </summary>
        public void Delete(IEnumerable<Entity> entities)
        {
            _context.Set<Entity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность указанного типа по уникальному идентификатору
        /// </summary>
        public void Delete(int id)
        {
            _context.Set<Entity>().Remove(Get(id));
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
