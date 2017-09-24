using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;

namespace YH.Face.Notification.Sms
{
  public  class SmsFace
    {

        private INotification _notification;

        public  SmsFacce(INotification notification)
        {
            _notification = notification;
        }
        public void SendSingle()
        {

            _notification.BatchSend();
        }
    }
}
