using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Http
{
   public class HttpAfterArgs:EventArgs
    {

        public HttpAfterArgs(string uri,string requestText,string responseText,
            string requestHeadText,
            short isSucess,
            DateTime requesttime,DateTime responseTime)
        {
            this.RequestText = requestText;
            this.ResponseText = responseText;
            this.RequestHeadText = requestHeadText;
            this.RequestTime = requesttime;
            this.ResponseTime = responseTime;
            this.IsSuccess = isSucess;
            this.Uri = uri;
        }

        /// <summary>
        /// 请求文本
        /// </summary>
       public string RequestText { get;private  set; }
        /// <summary>
        /// 请求远程路径
        /// </summary>
        public string Uri { get; private set; }
        
        public string RequestHeadText { get; private set; }

        public string ResponseText { get; private set; }

        public short IsSuccess { get; set; }

        public DateTime RequestTime { get; private set; }

        public DateTime ResponseTime { get; private set; }

        public double RunTime { get; private set; }
         
        /// <summary>
        /// 执行对象
        /// </summary>
        public string ExcuteObject { get; private  set; }
    }
}
