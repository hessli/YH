using System.Collections.Generic;

namespace YH.Core.Pagination
{
	/// <summary>
	/// 分页查询结果基础类,必须继承
	/// </summary>
	public class PagingResult<T>
	{
		/// <summary>
		/// Initializes
		/// </summary>
		/// <param name="pageSet">当前页数据集合</param>
		/// <param name="totalCount">总记录数</param>
		public PagingResult (IEnumerable<T> pageSet, int totalCount)
		{
			this._pageSet = pageSet;
			this._totalCount = totalCount;
		}

		IEnumerable<T> _pageSet = null;
		int _totalCount = 0;

		/// <summary>
		/// 当前页数据集合
		/// </summary>
		public IEnumerable<T> PageSet { 
			get {
				return this._pageSet;
			}
			set {
				this._pageSet = value;
			}
		}

		/// <summary>
		/// 总记录数
		/// </summary>
		public int TotalCount { 
			get {
				return this._totalCount;
			}
			set {
				this._totalCount = value;
			}
		}
	}
}

