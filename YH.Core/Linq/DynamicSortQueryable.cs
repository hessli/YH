using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Recursion.Family.Core.Utility.Linq
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum Order
    {
         Desc=1,

         Asc=2
    }
    public class Sort
    {
         public string Field { get; set; }

         public Order Order { get; set; }
    }
   public static class DynamicSortQueryable
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IList<Sort> sorts)
        {
            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(T), "o");
            string orderName = string.Empty;
            if (sorts != null && sorts.Count>0)
            {

                for (int i = 0; i < sorts.Count; i++)
                {
                    if (i == 0)
                        orderName = sorts[i].Order==Order.Desc ? "OrderByDescending" : "OrderBy";
                    else
                        orderName = sorts[i].Order==Order.Desc ? "ThenByDescending" : "ThenBy";
                    //根据属性名获取属性
                    var property = typeof(T).GetProperty(sorts[i].Field);
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                        orderName, new Type[] { typeof(T), property.PropertyType },
                        source.Expression, Expression.Quote(orderByExp));
                    source = source.Provider.CreateQuery<T>(resultExp);
                }
            }
            return source;
        }
    }
}
