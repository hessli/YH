using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification.Filter
{
    public class SendRecord
    {

        private DateTime _sendTime;

        private INotificationContent _content;
        public SendRecord(DateTime sendTime, INotificationContent content)
        {
            _sendTime = sendTime;

            _content = content;
        }
        public DateTime SendTime
        {
            get
            {

                return _sendTime;
            }
        }
        public INotificationContent Content
        {
            get
            {
                return _content;
            }
        }
    }
}
