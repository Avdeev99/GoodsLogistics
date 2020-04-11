using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoodsLogistics.BLL.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> Combine<T>(
            this List<Expression<Func<T, bool>>> expressions,
            Func<Expression, Expression, BinaryExpression> logicalFunction)
        {
            Expression<Func<T, bool>> filter = null;

            if (expressions.Count <= 0) return filter;
            var firstPredicate = expressions[0];
            var body = firstPredicate.Body;
            for (var i = 1; i < expressions.Count; i++)
                body = logicalFunction(body, Expression.Invoke(expressions[i], firstPredicate.Parameters));
            filter = Expression.Lambda<Func<T, bool>>(body, firstPredicate.Parameters);

            return filter;
        }
    }
}
