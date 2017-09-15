using Newtonsoft.Json;
using System;

namespace YH.Core.Serializable
{
    public class NewtonSerailizable : ISerializableObject
    {

        private JsonConverter[] _converts;

        public NewtonSerailizable()
        {

        }
        public NewtonSerailizable(params JsonConverter[] converts)
        {
            _converts = converts;
        }
        public T JsonDeserializeObject<T>(string jsonStr) where T : class, new()
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                throw new ArgumentNullException("jsonStr");
            }

            if (_converts != null && _converts.Length > 0)
            {
               return   JsonConvert.DeserializeObject<T>(jsonStr,_converts);
            }
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public string JsonSerializableObject<T>(T obj)
        {   
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
           return  JsonConvert.SerializeObject(obj);
        }
    }
}
