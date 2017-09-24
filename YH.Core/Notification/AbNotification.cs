using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification.Filter;

namespace YH.Core.Notification
{
    public abstract class AbNotification : INotification
    {

        public void Test()
        {
            BaseNotificationFilter filter;
 

        }

        public abstract void BatchSend(NotificationContentList contents);

        public abstract void BatchSend(NotificationTargetList targets, INotificationBody body);

        public abstract void MultiSendAsych(NotificationContentList contents);
       
        public abstract void BatchSendAsych(NotificationTargetList targets, INotificationBody body);

        public abstract void Send(INotificationTarget target, INotificationBody body);

        public abstract void SendAsych(INotificationTarget target, INotificationBody body);

    }
}
