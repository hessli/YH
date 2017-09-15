using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Logs
{
  public interface IYHLog
    {
        void Error(ILogParatmer paramter);
        void Info(ILogParatmer paramter);
        void ErrorAsych(ILogParatmer paramter);
        void InfoAsych(ILogParatmer paramter);
    }
}
