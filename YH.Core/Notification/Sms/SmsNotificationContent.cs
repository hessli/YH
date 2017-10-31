using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification.Sms
{
    public class SmsNotificationContent:INotificationContent
    {
        public SmsNotificationContent(SmsNotificationBody body, SmsNotificationTarget target)
        {
            if (target == null || string.IsNullOrWhiteSpace(target.Address))
                throw new ArgumentNullException("号码不可为空");

            if (body == null || string.IsNullOrWhiteSpace(body.Content))
            {
                throw new ArgumentNullException("内容不可为空");
            }

            Body = body;

            Target = target;
        }
 
        public INotificationBody  Body
        {
            get;
            private set;
        }

        public INotificationTarget Target
        {
            get;private set;
        }

     
    }
}
