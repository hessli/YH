using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using YH.Core.Serializable;
using YH.Core.Track;

namespace YH.Core.Http
{
   public class HttpWebRequestUtility
    {
         static readonly string CONTENT_TYPE_FORM = "application/x-www-form-urlencoded";
         static readonly string CONTENT_TYPE_JSON = "application/json";
         static readonly string HTTP_METHOD_GET = "GET";
         static readonly string HTTP_METHOD_POST = "POST";

        private StackTrace _stackTrace = null;
        private Tracked _traced = null;

        public HttpWebRequestUtility(string uri):this(uri,HttpMethod.GET,HttpContentType.FORM)
        {

        }

        public HttpWebRequestUtility(string uri,HttpMethod method, HttpContentType contentType) :this(uri,method,contentType,null,null,null)
        {

        }
        
        public HttpWebRequestUtility(string uri, HttpParameter parameter,HttpMethod method, HttpContentType contentType,
            HttpHead head) :this(uri,method,contentType,parameter,head,null)
        {
             
        } 
        public HttpWebRequestUtility(string uri, HttpParameter parameter):this(uri,HttpMethod.POST,HttpContentType.JSON, parameter,null, null)
        {
                
        }

        public HttpWebRequestUtility(string uri,
            HttpMethod method, 
            HttpContentType contentType,
           HttpParameter param,
            HttpHead head,
            ISerializableObject serializableService)
        {

            _stackTrace = new StackTrace();

            _traced = new  Tracked(_stackTrace);

            _head = head;


          
            if (_head == null)
            {
                _head = new HttpHead();
            }
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("uri");
            }
            if (method == HttpMethod.GET)
            {
                if (param != null && param.GetRequestParameterCount > 0)
                {
                    if (uri.IndexOf("?") == -1)
                    {
                        uri += "?";
                    }
                    uri += param.GetRequestParameter(HttpMethod.GET);
                }
            }
             
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;

            SetRequestHead(head);

            request.UserAgent = HttpHead.DEFAULT_USERAGENT;
            request.Timeout = head.TimeOut;

            if (method == HttpMethod.POST)
            {
                switch (contentType)
                {
                    case HttpContentType.FORM:
                        request.ContentType = CONTENT_TYPE_FORM;
                        break;
                    case HttpContentType.JSON:
                        request.ContentType = CONTENT_TYPE_JSON;
                        break;
                    default:
                        request.ContentType = CONTENT_TYPE_FORM;
                        break;
                }
                this._contentType = contentType;
            }

            request.Method = method == HttpMethod.GET ? HTTP_METHOD_GET : HTTP_METHOD_POST;

            this._parameter = param;

            this._method = method;

            this._request = request;

            this._serializableService = serializableService;
        }

        HttpHead _head = null;
        HttpParameter _parameter = null;
        HttpMethod _method = HttpMethod.POST;
        HttpContentType _contentType = HttpContentType.FORM;
        HttpWebRequest _request = null;
        ISerializableObject _serializableService = null;

        private void SetRequestHead(HttpHead head)
        {
            _request.Accept = head.Accept;

            _request.Headers.Add(head.HeadCollection);
        }

        byte[] GetRequestStream()
        {
            byte[] bytes = null;
            if (this._parameter != null)
            {
                string text = this._parameter.GetRequestParameter(_method); ;
               
                bytes = this._head.Encoding.GetBytes(text);
            }
            return bytes;
        }

        public Stream RequestStream()
        {
            var stream = default(Stream);
            if (this._method == HttpMethod.POST)
            {
                byte[] bytes = this.GetRequestStream();
                this._request.ContentLength = bytes == null ? 0 : bytes.Length;
                if (bytes != null)
                {
                    stream = this._request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            WebResponse response = this._request.GetResponse();
            if (response != null)
            {
                var responseStream = response.GetResponseStream();
                return responseStream;
            }
            return default(Stream);
        }


        /// <summary>
        /// 发送请求并返回响应
        /// </summary>
        /// <returns></returns>
        public string Request()
        {
            if (this._method == HttpMethod.POST)
            {
                byte[] bytes = this.GetRequestStream();
                if (bytes != null)
                {
                    this._request.ContentLength = bytes.Length;
                    Stream stream = this._request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            WebResponse response = this._request.GetResponse();
            if (response != null)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), this._head.Encoding);


                return sr.ReadToEnd().Trim();
            }
            return null;
        }

        /// <summary>
        /// 发送请求并返回响应
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Request<T>()
            where T : class, new()
        {

            var result = default(T);
            if (this._method == HttpMethod.POST)
            {
                byte[] bytes = this.GetRequestStream();
                if (bytes != null)
                {
                    this._request.ContentLength = bytes.Length;
                    Stream stream = this._request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            WebResponse response = this._request.GetResponse();
            if (response != null)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), this._head.Encoding);
                string text = sr.ReadToEnd().Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    if (this._serializableService == null)
                    {
                        result = new NewtonSerailizable().JsonDeserializeObject<T>(text);
                    }
                    else
                    {
                        result = _serializableService.JsonDeserializeObject<T>(text);
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// 异步请求
        /// </summary>
        public void ReuestAsynch()
        {
            if (this._method == HttpMethod.POST)
            {
                byte[] bytes = this.GetRequestStream();
                if (bytes != null)
                {
                    this._request.ContentLength = bytes.Length;
                    Stream stream = this._request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            this._request.GetResponseAsync();

        } 
    }
}
