using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Http
{
    public class HttpHead
    {
        static readonly string DEFAULT_USERAGENT_PREFIX = "YH";
        /// <summary>
        /// 当前程序集版本
        /// </summary>
        internal static Version CURRENT_VERSION = Assembly.GetExecutingAssembly().GetName().Version;

        internal static readonly string DEFAULT_USERAGENT = string.Empty;

        static HttpHead()
        {
            var useragent_prefix = ConfigurationManager.AppSettings["Agent_Prefix"];

            if (string.IsNullOrWhiteSpace(useragent_prefix))
            {
                useragent_prefix = DEFAULT_USERAGENT_PREFIX;
            }

            DEFAULT_USERAGENT = useragent_prefix + "(" + (CURRENT_VERSION == null ? "1.0.0.0" : CURRENT_VERSION.ToString()) + "),Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)";
        }

        private NameValueCollection _headCollection;
        public NameValueCollection HeadCollection
        {
            get
            {
                return _headCollection;
            }
        }


        public HttpHead()
        {
            _headCollection = new NameValueCollection();
            _hasOptionsSetValue = new HashSet<string>();

            _hasOptionsSetValue.Add("accecept");
            _hasOptionsSetValue.Add("useragent");
            _hasOptionsSetValue.Add("timout");
        }
        public HttpHead(string accept) : base()
        {

            _accept = accept;


        }

        private string _accept;
        public string Accept
        {
            get
            {
                return _accept;
            }
        }


        private string _userAgent;
        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
            set
            {
                _userAgent = value;
            }
        }

        private Encoding _encoding = Encoding.UTF8;
        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }

        private int _timeOut = 30000;
        public int TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
            }
        }

        private HashSet<string> _hasOptionsSetValue;
        public void Add(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name 不能为空");

            if (_hasOptionsSetValue.Contains(name))
                throw new ArgumentException(string.Format("参数{0}只能通过当前类的构造函数赋值不可新增", name));

            this._headCollection.Add(name, value);


        }



    }
}
