using System;

namespace System.Linq.Expressions
{
	public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
	{
		private ISpecification<T> _LeftSideSpecification;
		private ISpecification<T> _RightSideSpecification;

		public OrSpecification (ISpecification<T> leftSideSpec, ISpecification<T> rightSideSpec)
		{
			if (leftSideSpec == null) {
				throw new ArgumentNullException ("leftSideSpec");
			}
			if (rightSideSpec == null) {
				throw new ArgumentNullException ("rightSideSpec");
			}
			this._LeftSideSpecification = leftSideSpec;
			this._RightSideSpecification = rightSideSpec;
		}

		public override Expression<Func<T, bool>> SatisfiedBy ()
		{
			Expression<Func<T, bool>> first = this._LeftSideSpecification.SatisfiedBy ();
			Expression<Func<T, bool>> second = this._RightSideSpecification.SatisfiedBy ();
			return first.Or<T> (second);
		}

		public override ISpecification<T> LeftSideSpecification {
			get {
				return this._LeftSideSpecification;
			}
		}

		public override ISpecification<T> RightSideSpecification {
			get {
				return this._RightSideSpecification;
			}
		}
	}
}