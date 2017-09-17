
namespace System.Linq.Expressions
{
	public abstract class CompositeSpecification<T> : Specification<T> where T : class
	{
		protected CompositeSpecification ()
		{
		}

		public abstract ISpecification<T> LeftSideSpecification { get; }

		public abstract ISpecification<T> RightSideSpecification { get; }
	}
}
