using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace YH.Core.DataProvider
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private IUnitOfWork _masterOfWork;
		private IUnitOfWork _subOfWork;

		/// <summary>
		/// Initializes a new instance of the <see cref="Recursion.Family.Core.Repository`1"/> class.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		public Repository (IUnitOfWork unitOfWork)
		{
			this._masterOfWork = unitOfWork;
			this._subOfWork = unitOfWork;
		}

		/// <summary>
		/// 主从读写分离,查询操作使用从库
		/// </summary>
		/// <param name="masterUnitOfWork">主库工作单元</param>
		/// <param name="subUnitOfWork">从库工作单元</param>
		public Repository (IUnitOfWork masterUnitOfWork, IUnitOfWork subUnitOfWork)
		{
			this._masterOfWork = masterUnitOfWork;
			this._subOfWork = subUnitOfWork;
		}

		public IQueryable<T> GetList (ISpecification<T> spec)
		{
			IQueryable<T> source = this.Entities.Where<T> (spec.SatisfiedBy ());
			return source;
		}

		public IQueryable<T> GetList (Expression<Func<T, bool>> expression)
		{
        
			return this.Entities.Where<T> (expression);
		}

		public IQueryable<T> GetList<S> (ISpecification<T> spec, Expression<Func<T, S>> orderBy, bool descending)
		{
			var source = GetList (spec);
			return descending ? source.OrderByDescending<T, S> (orderBy) : source.OrderBy<T, S> (orderBy);
		}

		public IQueryable<T> GetList<S> (Expression<Func<T, bool>> expression, Expression<Func<T, S>> orderBy, bool descending)
		{
			var source = GetList (expression);
			return descending ? source.OrderByDescending<T, S> (orderBy) : source.OrderBy<T, S> (orderBy);
		}

		public T GetFirstOrDefault (ISpecification<T> spec)
		{
			return this.Entities.Where<T> (spec.SatisfiedBy ()).FirstOrDefault<T> ();
		}

		public T GetFirstOrDefault (Expression<Func<T, bool>> expression)
		{
			return this.Entities.Where<T> (expression).FirstOrDefault<T> ();
		}

        public DbRawSqlQuery<TElement> ExcuteSqlQuery<TElement>(string sql, params object[] paramters)
        {
            return this.UnitOfWork.ExcuteSqlQuery<TElement>(sql,paramters);
        }

        public IQueryable<T> Entities {
			get {
				return this._subOfWork.Set<T> ();
			}
		}

		public IUnitOfWork UnitOfWork {
			get {
				return this._masterOfWork;
			}
		}
	}
}

