using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Comm;

namespace YH.Core.Logs
{
   public interface ILogParatmer
    {
        
         string Paramters { get;  }

         string ExcuteResult { get;}

         DateTime RequestTime { get; }

         DateTime ResponseTime { get; }

         double RunTime { get; }
        /// <summary>
        /// 日志类型
        /// </summary>
        LogType Type { get;  }
         
        int Module { get; }
         
         /// <summary>
         /// 执行对象
         /// </summary>
         string ExcuteObject { get;  }
         short Level { get;  }
        string Formart();
    }
}
