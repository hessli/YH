using System;

namespace System.Linq.Expressions
{
	public interface ISpecification<T> where T : class
	{
		Expression<Func<T, bool>> SatisfiedBy ();
	}
}
