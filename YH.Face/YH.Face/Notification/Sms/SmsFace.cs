using System.Collections.Generic;
using System.Linq;
using YH.Core.Logs;
using YH.Core.Notification;
using YH.Core.Notification.Sms;
using YH.Service.Notification.Sms;
using YH.Service.Notification.Sms.YunPian;

namespace YH.Face.Notification.Sms
{
    public  class SmsFace
    {
        private Dictionary<string, IList<INotification>> _dic;

        static readonly string DEFULTSENDCODE = "00";

        private readonly IYHLog _yhLog;

        public SmsFace(IYHLog yhLog)
        {
            _yhLog = yhLog;


            _dic = new Dictionary<string, IList<INotification>>();

            IList<INotification> china = new List<INotification>();

            IList<INotification> foregin = new List<INotification>();

            china.Add(new YPChinaNotification(_yhLog));

            foregin.Add(new YPForeignNotification(_yhLog));

            _dic.Add("86",china);

            _dic.Add(DEFULTSENDCODE, foregin);
        }

        public void SendSingle(string areacode, string mobile,string text)
        {
            SmsNotificationTarget target = new SmsNotificationTarget(areacode,mobile);

            SmsNotificationBody body = new SmsNotificationBody(text);

            IList<INotification> notification = null;

            _dic.TryGetValue(areacode, out notification);

            if (notification == null)
            {
                notification = _dic[DEFULTSENDCODE];
            }

            SendSmsFilter.Instance.Filter(mobile,text);

            foreach (var item in notification)
            {
                 item.Send(target, body);

                break;
            }

            SendSmsFilter.Instance.Update(mobile, text);
        }

        public void BatchSend(SmsNotificationTargetList targets, 
            SmsNotificationBody body)
        {
            foreach (var item in targets.AreaCodes)
            {
                var items = targets[item];
                IList<INotification> notifications = null;

                if (!_dic.TryGetValue(item, out notifications))
                {
                    notifications = _dic[DEFULTSENDCODE];
                }
                if (items != null)
                {
                    foreach (var subItem in notifications)
                    {
                        subItem.BatchSend(items.Cast<INotificationTarget>().ToList(), body);
                        break;
                    }
                }
            }
        }

        public void MuliSend(SmsNotificationContentList contents)
        {
            
                SendSmsFilter.Instance.Filter(contents);

                foreach (var item in contents.AreaCodes)
                {
                    var items = contents[item];

                    IList<INotification> notifications = null;

                    if (!_dic.TryGetValue(item, out notifications))
                    {
                        notifications = _dic[DEFULTSENDCODE];
                    }

                    if (items != null && notifications!=null && notifications.Count>0)
                    {
                        foreach (var subItem in notifications)
                        {
                            subItem.MultiSend((items.Cast<INotificationContent>()).ToList());
                           
                            break;
                        }
                        SendSmsFilter.Instance.Update(contents);
                    }
                }
          
        }
    }
}
