using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Logs;
using YH.Core.Notification;
using YH.Core.Notification.Sms;
using YH.Core.Serializable;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPForeignNotification : YPBaseSmsNotification, INotification
    {
        static readonly string SINGLEURI = "v2/sms/single_send.json";
        public YPForeignNotification():base()
        {
        }
        public YPForeignNotification(IYHLog yhLog) : base(yhLog)
        {

        }
        public void BatchSend(IList<INotificationTarget> targets, INotificationBody body)
        {
            BatchSendAsych(targets, body);
        }

        public void BatchSendAsych(IList<INotificationTarget> targets, INotificationBody body)
        {
            IList<INotificationContent> contents = new List<INotificationContent>();
            foreach (var item in targets)
            {
                contents.Add(new SmsNotificationContent((SmsNotificationBody)body, (SmsNotificationTarget)item));
            }
            //自动换为异步发送
            this.MultiSendAsych(contents);
        }

        public void MultiSend(IList<INotificationContent> contents)
        {
            //自动转换为异步发送
            this.MultiSendAsych(contents);
        }

        public void MultiSendAsych(IList<INotificationContent> contents)
        {
            Task.Run(() =>
            {

                foreach (var item in contents)
                {
                    Send(item.Target, item.Body);

                    Thread.Sleep(1);
                }
            });
        }

        public override void Send(INotificationTarget target, INotificationBody body)
        {
            HttpParameter paramter = new HttpParameter();

            paramter.SetPostParameter(body);

            ISerializableObject serializableObject = new YPSerailizable(base.ApiKey, target, body);

            var httpRequest = new HttpWebRequestUtility(base.BaseUri + SINGLEURI, paramter, HttpMethod.POST, HttpContentType.JSON, serializableObject);

            httpRequest.AfterRequest += base.HttpRequest_AfterRequest;

            httpRequest.Request();
        }

        public void SendAsych(INotificationTarget target, INotificationBody body)
        {
            Task.Run(() =>
            {
                Send(target, body);

            });
        }
    }
}
