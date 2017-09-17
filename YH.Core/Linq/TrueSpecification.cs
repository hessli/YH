using System;

namespace System.Linq.Expressions
{
	public sealed class TrueSpecification<T> : Specification<T> where T : class
	{
		public override Expression<Func<T, bool>> SatisfiedBy ()
		{
			ParameterExpression expression2;
			bool result = true;
			Expression<Func<T, bool>> exp = Expression.Lambda<Func<T, bool>> (Expression.Constant (result), new ParameterExpression[] { expression2 = Expression.Parameter (typeof(T), "t") });

			return exp;
		}
	}
}
