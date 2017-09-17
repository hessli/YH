using System;

namespace System.Linq.Expressions
{
	public abstract class Specification<T> : ISpecification<T> where T : class
	{
		protected Specification ()
		{
		}

		public static Specification<T> operator & (Specification<T> leftSideSpec, Specification<T> rightSideSpec)
		{
			return new AndSpecification<T> (leftSideSpec, rightSideSpec);
		}

		public static Specification<T> operator | (Specification<T> leftSideSpec, Specification<T> rightSideSpec)
		{
			return new OrSpecification<T> (leftSideSpec, rightSideSpec);
		}

		public static bool operator false (Specification<T> specification)
		{
			return false;
		}

		public static Specification<T> op_LogicalNot (Specification<T> specification)
		{
			return new NotSpecification<T> (specification);
		}

		public static bool operator true (Specification<T> specification)
		{
			return true;
		}

		public abstract Expression<Func<T, bool>> SatisfiedBy ();
	}
}