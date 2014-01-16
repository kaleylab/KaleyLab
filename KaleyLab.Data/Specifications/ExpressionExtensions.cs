using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KaleyLab.Data.Specifications
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            ParameterExpression expression = left.Parameters[0];
            if (object.ReferenceEquals(expression, right.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, right.Body), new ParameterExpression[] { expression });
            }
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, Expression.Invoke(right, new Expression[] { expression })), new ParameterExpression[] { expression });
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            ParameterExpression expression = left.Parameters[0];
            if (object.ReferenceEquals(expression, right.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, right.Body), new ParameterExpression[] { expression });
            }
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, Expression.Invoke(right, new Expression[] { expression })), new ParameterExpression[] { expression });
        }
    }
}
