using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetMany(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>> orderBy = null,
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes);

        T Get(
            Expression<Func<T, bool>> condition,
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes);

        IEnumerable<T> GetAll(
            TrackingState trackingState = TrackingState.Enabled,
            params string[] includes);

        T Create(T item);

        T Update(T item);

        void Delete(string id);

        bool IsExist(Expression<Func<T, bool>> condition);
    }
}
