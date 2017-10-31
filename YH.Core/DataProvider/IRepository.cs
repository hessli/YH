using System;
using System.Linq;
using System.Linq.Expressions;

namespace YH.Core.DataProvider
{
    /// <summary>
    /// 数据持久层
    /// </summary>
    public interface IRepository<T>  where T :class
	{
        //DbRawSqlQuery<TElement> ExcuteSqlQuery<TElement>(string sql, params object[] paramters);

        IQueryable<T> GetList (ISpecification<T> spec);

		IQueryable<T> GetList (Expression<Func<T, bool>> expression);

		IQueryable<T> GetList<S> (ISpecification<T> spec, Expression<Func<T, S>> orderBy, bool descending);

		IQueryable<T> GetList<S> (Expression<Func<T, bool>> expression, Expression<Func<T, S>> orderBy, bool descending);

		T GetFirstOrDefault (ISpecification<T> spec);

		T GetFirstOrDefault (Expression<Func<T, bool>> expression);

		IQueryable<T> Entities { get; }

		IUnitOfWork UnitOfWork{ get; }
	}
}

