using System.Collections.Generic;
using YH.Core.Pagination;
namespace System.Linq
{
    /// <summary>
    /// Linq扩展类
    /// </summary>
    public static class LinqExtensions
	{
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static PagingResult<T> Pagination<T> (this IQueryable<T> query, int pageIndex, int pageSize)
		{
			int count = 0;
			IEnumerable<T> pageSet = new List<T> ();

			if (query != null) {
				pageSet = query.Skip ((pageIndex - 1) * pageSize).Take (pageSize).ToList ();
				count = query.Count ();
			}
			return new PagingResult<T> (pageSet, count);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static PagingResult<T> Pagination<T> (this IList<T> list, int pageIndex, int pageSize)
		{
			int count = 0;
			List<T> pageSet = null;

            if (list != null && list.Count > 0)
            {
                pageSet = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = list.Count;
            }
            else 
                pageSet = new List<T>();
			return new PagingResult<T> (pageSet, count);
		}
	}
}

