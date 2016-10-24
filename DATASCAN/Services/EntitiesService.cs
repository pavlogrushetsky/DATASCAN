using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DATASCAN.Core.Model.Common;
using DATASCAN.Repositories;

namespace DATASCAN.Services
{
    /// <summary>
    /// Сервис для доступа к данным сущностей
    /// </summary>
    public class EntitiesService<T> where T : EntityBase
    {
        protected readonly string _connection;

        /// <summary>
        /// Сервис для доступа к данным сущностей
        /// </summary>
        /// <param name="connection">Строка соединения</param>
        public EntitiesService(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Возвращает коллекцию сущностей
        /// </summary>
        /// <param name="predicate">Утверждение для фильтрации возвращаемых сущностей</param>
        /// <param name="onException">Действие, выполняемое при исключении</param>
        /// <param name="order">Поле, по которому осуществляется сортировка</param>
        /// <param name="include">Набор включаемых связанных сущностей</param>
        /// <returns></returns>
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, Action<Exception> onException = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, params Expression<Func<T, object>>[] include)
        {
            List<T> estimators = null;

            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<T> repo = new EntityRepository<T>(_connection))
                {
                    IQueryable<T> entities = repo.GetAll();

                    if (include != null)
                    {
                        entities = include.Aggregate(entities, (e, incl) => e.Include(incl));
                    }

                    if (predicate != null)
                    {
                        entities = entities.Where(predicate);
                    }

                    if (order != null)
                    {
                        entities = order(entities);
                    }

                    estimators = entities.ToList();
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return estimators;
        }

        /// <summary>
        /// Обновляет данные сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="onSuccess">Действие, выполняемое в случае успешного обновления</param>
        /// <param name="onException">Действие, выполняемое в случае исключения</param>
        public virtual async Task Update(T entity, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<T> repo = new EntityRepository<T>(_connection))
                {
                    entity.DateModified = DateTime.Now;
                    repo.Update(entity);
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Обновляет данные сущностей
        /// </summary>
        /// <param name="entities">Сущности</param>
        /// <param name="onSuccess">Действие, выполняемое в случае успешного обновления</param>
        /// <param name="onException">Действие, выполняемое в случае исключения</param>
        public virtual async Task Update(List<T> entities, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                entities.ForEach(e => e.DateModified = DateTime.Now);

                using (EntityRepository<T> repo = new EntityRepository<T>(_connection))
                {
                    repo.Update(entities);
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Добавляет данные сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="onSuccess">Действие, выполняемое в случае успешного обновления</param>
        /// <param name="onException">Действие, выполняемое в случае исключения</param>
        public async Task Insert(T entity, Action onSuccess = null, Action<Exception> onException = null)
        {
            await Task.Factory.StartNew(() =>
            {
                using (EntityRepository<T> repo = new EntityRepository<T>(_connection))
                {
                    repo.Insert(entity);
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(result =>
            {
                if (result.Exception != null)
                {
                    onException?.Invoke(result.Exception.InnerException);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}