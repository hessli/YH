using System;

namespace System.Linq.Expressions
{
	public sealed class DirectSpecification<T> : Specification<T> where T : class
	{
		private Expression<Func<T, bool>> _MatchingCriteria;

		public DirectSpecification (Expression<Func<T, bool>> matchingCriteria)
		{
			if (matchingCriteria == null) {
				throw new ArgumentNullException ("matchingCriteria");
			}
			this._MatchingCriteria = matchingCriteria;
		}

		public override Expression<Func<T, bool>> SatisfiedBy ()
		{
			return this._MatchingCriteria;
		}
	}
}
