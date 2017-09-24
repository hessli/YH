using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification
{
    public interface INotificationContent
    {
          INotificationTarget Target { get; }
          INotificationBody Body { get; }
    }
}
