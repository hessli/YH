using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq.Expressions
{
    /// <summary>
    /// 集合重复
    /// </summary>
    public static class DistinctExtensions
    {
        /// <summary>
        /// 通过使用指定的 System.Collections.Generic.IEqualityComparer<T> 对值进行比较返回序列中的非重复元素。
        /// </summary>
        /// <typeparam name="T">source 中的元素的类型。</typeparam>
        /// <typeparam name="V">比较的类型</typeparam>
        /// <param name="source">要从中移除重复元素的序列。</param>
        /// <param name="keySelector">应用于每个元素的比较</param>
        /// <returns>一个 System.Collections.Generic.IEnumerable<T>，包含源序列中的非重复元素。</returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }
    }
}
