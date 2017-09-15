using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Serializable;

namespace YH.Core.Http
{
    public class HttpParameter
    {
        private Dictionary<string, object> _dict;

        private object _parameter;

        public HttpParameter()
        {
            _dict = new Dictionary<string, object>();
        }

        internal string GetRequestParameter(HttpMethod method, ISerializableObject serializable = null)
        {
            string requestParaStr = string.Empty;
            switch (method)
            {
                case HttpMethod.GET:
                    requestParaStr = GetRequestParatmer();
                    break;
                case HttpMethod.POST:
                    this.PostRquestParamter(serializable);
                    break;
                default:
                    throw new ArgumentException("不支持的请求方式");
            }

            return requestParaStr;
        }

        private string _serializeParameters = string.Empty;
        public string SerializeParameters
        {
            get
            {
                return _serializeParameters;
            }
        }
        private string PostRquestParamter(ISerializableObject serializable)
        {
            if (_parameter != null)
            {
                if (serializable != null)
                {
                    _serializeParameters = serializable.JsonSerializableObject(_parameter);
                }
                else
                {
                    var newtown = new NewtonSerailizable();

                    _serializeParameters = newtown.JsonSerializableObject(_parameter);
                }
            }
            return _serializeParameters;
        }
        private string GetRequestParatmer()
        {
            int len = this._dict.Count;
            if (len == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (var item in this._dict)
            {
                if (i < len)
                {
                    sb.AppendFormat("{0}={1}&", item.Key, item.Value);
                    i++;
                }
                else
                {
                    sb.AppendFormat("{0}={1}", item.Key, item.Value);
                }
            }
            _serializeParameters=sb.ToString();

            return _serializeParameters;
        }


        /// <summary>
        /// get 请求参数个数
        /// </summary>
        internal int GetRequestParameterCount
        {
            get {
                return _dict.Count;
            }
        }

        public void SetPostParameter(object value)
        {
            _parameter = value;
        }
        public void AddGetParameter(string name, object value)
        {
            _dict.Add(name, value);
        }

        internal HttpAfterArgs ToAfterArgs()
        {
            var args = new HttpAfterArgs {


            };

        }
    }
}
