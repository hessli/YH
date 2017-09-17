using System;
using System.Linq;

namespace System.Linq.Expressions
{
	public static class ExpressionBuilder
	{
		public static Expression<T> Compose<T> (this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			Expression expression = ParameterRebinder.ReplaceParameters (
				                        first.Parameters.Zip (
					                        second.Parameters,
					                        (f, s) => new
                    {
                        First = f,
                        Second = s
                    }).ToDictionary (p => p.Second, p => p.First),
				                        second.Body);
			return Expression.Lambda<T> (merge (first.Body, expression), first.Parameters);
		}

		public static Expression<Func<T, bool>> Or<T> (this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose<Func<T, bool>> (second, new Func<Expression, Expression, Expression> (Expression.Or));
		}

		public static Expression<Func<T, bool>> And<T> (this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			return first.Compose<Func<T, bool>> (second, new Func<Expression, Expression, Expression> (Expression.And));
		}
	}
}
