using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification.Sms
{
    public class SmsNotificationBody : INotificationBody
    {
        public SmsNotificationBody(string content)
        {
            Content = content;
        }
        public string Content
        {
            get;
            private set;
        }
    }
}
