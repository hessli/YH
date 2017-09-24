using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms
{
    public abstract class SmsBaseNotificationBody : INotificationBody
    {
        public string Content
        {
            get;
            protected set;
        }
    }
}
