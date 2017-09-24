using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms
{
  public  interface ISendHttpRequest
    {
        HttpWebRequestUtility SingleSendHttpRequest(INotificationTarget target, INotificationBody body);

        IList<HttpWebRequestUtility> BatchSendHttpRequest(NotificationTargetList targets, INotificationBody body);

        IList<HttpWebRequestUtility> MultiSendHttpRequest(IList<INotificationContent> contents);
    }
}
