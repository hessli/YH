using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Http
{
   public class HttpAfterArgs:EventArgs
    {

        public HttpAfterArgs(string paramters,string uri,string excuteResult,
            DateTime requesttime,DateTime responseTime)
        {
              
        }

       public string Paramters { get;private  set; }
        /// <summary>
        /// 请求远程路径
        /// </summary>
        public string Uri { get; private set; }

        public string ExcuteResult { get; private set; }

        public DateTime RequestTime { get; private set; }

        public DateTime ResponseTime { get; private set; }

        public double RunTime { get; private set; }

         
        /// <summary>
        /// 执行对象
        /// </summary>
        public string ExcuteObject { get; private  set; }
    }
}
