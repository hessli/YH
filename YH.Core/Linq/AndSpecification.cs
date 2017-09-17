namespace System.Linq.Expressions
{
    public class AndSpecification<T> : CompositeSpecification<T> where T : class
	{
		private ISpecification<T> _LeftSideSpecification;
		private ISpecification<T> _RightSideSpecification;

		public AndSpecification (ISpecification<T> leftSideSpec, ISpecification<T> rightSideSpec)
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
			return this._LeftSideSpecification.SatisfiedBy ().And<T> (this._RightSideSpecification.SatisfiedBy ());
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
