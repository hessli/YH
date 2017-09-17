using System;
using System.Collections.Generic;

namespace System.Linq.Expressions
{

    /// <summary>
    ///定义方法以支持对象的相等比较。 
    /// </summary>
    /// <typeparam name="T">比较对象类型</typeparam>
    /// <typeparam name="TV">应用于每个元素的比较的类型</typeparam>
    public class CommonEqualityComparer<T, TV> : IEqualityComparer<T>
    {
        private readonly Func<T, TV> _keySelector;

        public CommonEqualityComparer(Func<T, TV> keySelector)
        {
            _keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<TV>.Default.Equals(_keySelector(x), _keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<TV>.Default.GetHashCode(_keySelector(obj));
        }
    }
}
