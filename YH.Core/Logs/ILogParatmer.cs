using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Logs
{
   public interface ILogParatmer
    {
         string Paramters { get; set; }

         string ExcuteResult { get; set; }

         DateTime RequestTime { get; set; }

         DateTime ResponseTime { get; set; }

         double RunTime { get; set; }
         /// <summary>
         /// 模块
         /// </summary>
         int? Moudel { get; set; }

         /// <summary>
         /// 执行对象
         /// </summary>
         string ExcuteObject { get; set; }
    }
}
