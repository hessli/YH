using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Comm;
using YH.Core.Logs;

namespace YH.Core.Http
{
    public class HttpRequestLog : ILogParatmer
    {
        public HttpRequestLog(string excuteObject,string responseText,
            short level,
            int module,
            short issuccess,
            string uri,
            string paramters,
            DateTime requestTime,
            DateTime responseTime,
            LogType logType)
        {

            _excuteObject = excuteObject;

            _isSuccess = issuccess;

            _responseText = responseText;

            _level = level;

            _paramters = paramters;

            _requestTime = requestTime;

            _responseTime = responseTime;

            _module = module;

            _uri = uri;

            _excuteResult = excuteObject;

            _type = logType;
        }

        private string _responseText;
        public string ResponseText
        {
            get {
                return _responseText;
            }
        }

        private string _excuteObject;
        public string ExcuteObject
        {
            get
            {
                return _excuteObject??string.Empty;
            }
        }

        private string _uri;
        public string  Path { get {

                return _uri??string.Empty;
            } }

        private string _excuteResult;
        public string ExcuteResult
        {
            get
            {
                return _excuteResult;
            }
        }

        private short _level;
        public short Level
        {
            get
            {
                return _level;
            }
        }

        private int _module;
        public int Module
        {
            get
            {
                return _module;
            }
        }

        private string _paramters;
        public string Paramters
        {
            get
            {
                return _paramters;
            }
        }

        private DateTime _requestTime;

        public DateTime RequestTime
        {
            get
            {
                return _requestTime;
            }
        }

        private DateTime _responseTime;
        public DateTime ResponseTime
        {
            get
            {
                return _responseTime;
            }
        }

        private double _runTime;
        public double RunTime
        {
            get
            {
                var timeLong = _responseTime - _requestTime;

                _runTime = timeLong.TotalMilliseconds;

                return _runTime;
            }
        }

        private LogType _type;

        public LogType Type
        {
            get
            {
                return _type;
            }
        }

        private short _isSuccess;

        public short IsSuccess { get; }

     

        public string Formart()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("---------------模块{0},请求日志请求结果:{1},请求路径:{2}-------------------",this._module,this._isSuccess,this._uri);

            builder.AppendLine(string.Format(" 开始时间:{0},结束时间为:{1} 总时长:{2}", this._requestTime, this._responseTime, this._runTime));

            builder.AppendLine(string.Format("请求参数:{0}",this._paramters));

            builder.AppendLine(string.Format("响应内容为:{0}", this.ExcuteResult));

            return builder.ToString();
        }
    }
}
