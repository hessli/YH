using System;
using System.Linq;

namespace System.Linq.Expressions
{
	public sealed class NotSpecification<T> : Specification<T> where T : class
	{
		private Expression<Func<T, bool>> _OriginalCriteria;

		public NotSpecification (ISpecification<T> originalSpecification)
		{
			if (originalSpecification == null) {
				throw new ArgumentNullException ("originalSpecification");
			}
			this._OriginalCriteria = originalSpecification.SatisfiedBy ();
		}

		public NotSpecification (Expression<Func<T, bool>> originalSpecification)
		{
			if (originalSpecification == null) {
				throw new ArgumentNullException ("originalSpecification");
			}
			this._OriginalCriteria = originalSpecification;
		}

		public override Expression<Func<T, bool>> SatisfiedBy ()
		{
			return Expression.Lambda<Func<T, bool>> (Expression.Not (this._OriginalCriteria.Body), new ParameterExpression[] { this._OriginalCriteria.Parameters.Single<ParameterExpression> () });
		}
	}
}
