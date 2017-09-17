using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Logs
{

  public  delegate void NotificationHandler(object sender,object value);

  public interface IYHLog
    {  

        event NotificationHandler Notification;

        void Error(ILogParatmer paramter);
        void Info(ILogParatmer paramter);
        void ErrorAsych(ILogParatmer paramter);
        void InfoAsych(ILogParatmer paramter);
        
    }
}
