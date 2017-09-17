using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace YH.Core.DataProvider
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// 不会立即提交,只会保存到当前上下文Context对象中
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void Add<T> (T entity) where T : class;

		/// <summary>
		/// 不会立即提交,只会保存到当前上下文Context对象中
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void Update<T> (T entity) where T : class;

		/// <summary>
		/// 不会立即提交,只会保存到当前上下文Context对象中
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void Remove<T> (T entity) where T : class;
        
		IQueryable<T> Set<T> () where T : class;

        /// <summary>
        /// 执行sql查询，
        /// 这个方法放在这个类有点脏
        /// 
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        DbRawSqlQuery<TElement> ExcuteSqlQuery<TElement>(string sql,params object[] paramters);
        /// <summary>
        /// 立即提交当前上下文Context,抛异常会自动回滚(事务式提交)
        /// </summary>
        bool Commit ();

		/// <summary>
		/// 立即执行一段sql语句,并返回受影响的行数(非事务)
		/// </summary>
		/// <returns>The SQ.</returns>
		/// <param name="sql">Sql.</param>
		/// <param name="parameters">Parameters.</param>
		int ExecuteSQL (string sql, params object[] parameters);
	}
}

