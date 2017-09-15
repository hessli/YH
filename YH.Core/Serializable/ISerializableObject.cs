using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Serializable
{
    public interface ISerializableObject
    {

        
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns>The deserialize object.</returns>
        /// <param name="jsonStr">Json string.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T JsonDeserializeObject<T>(string jsonStr) where T : class, new();
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>The serializable object.</returns>
        /// <param name="obj">Object.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        string JsonSerializableObject<T>(T obj);
    }
}
