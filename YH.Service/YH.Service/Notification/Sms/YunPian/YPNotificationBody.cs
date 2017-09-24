using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPNotificationBody : SmsBaseNotificationBody
    { 
        public YPNotificationBody(string content)
        {
            Content = content;
        }
    }
}
