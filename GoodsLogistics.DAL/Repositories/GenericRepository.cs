using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodsLogistics.DAL.EF;
using GoodsLogistics.DAL.Repositories.Interfaces;
using GoodsLogistics.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoodsLogistics.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GoodsLogisticsContext context)
        {
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes)
        {
            var result = trackingState == TrackingState.Enabled ? _dbSet : _dbSet.AsNoTracking();
            result = includes.Aggregate(result, (current, include) => current.Include(include));

            return result;
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> filter,
            Expression<Func<T, object>> orderBy = null,
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes)
        {
            IQueryable<T> result = includes.Aggregate<string, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));
            result = trackingState == TrackingState.Enabled ? result.Where(filter) : result.AsNoTracking().Where(filter);

            if (orderBy != null)
            {
                result = result.OrderBy(orderBy);
            }

            return result;
        }

        public T Get(Expression<Func<T, bool>> condition,
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes)
        {
            IQueryable<T> result = includes.Aggregate<string, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));

            return trackingState == TrackingState.Enabled
                ? result.FirstOrDefault(condition)
                : result.AsNoTracking().FirstOrDefault(condition);
        }


        public T Create(T item)
        {
            var createdItem = _dbSet.Add(item);
            return createdItem.Entity;
        }

        public T Update(T item)
        {
            var updatedItem = _dbSet.Update(item);
            return updatedItem.Entity;
        }

        public void Delete(string id)
        {
            var item = _dbSet.Find(id);
            _dbSet.Remove(item);
        }

        public bool IsExist(Expression<Func<T, bool>> condition)
        {
            return _dbSet.Any(condition);
        }
    }
}
