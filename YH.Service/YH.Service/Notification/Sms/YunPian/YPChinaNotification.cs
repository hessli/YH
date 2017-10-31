using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Logs;
using YH.Core.Notification;
using YH.Core.Notification.Sms;
using YH.Core.Serializable;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPChinaNotification : YPBaseSmsNotification, IChinaSmsNotification
    {
        private HttpWebRequestUtility _httpRequest;
    
        static readonly string SINGLEURI = "v2/sms/single_send.json";

        static readonly string BATCHURI = "/v2/sms/batch_send.json";

        static readonly string MULTIURI = "v2/sms/multi_send.json";

        private INotificationAddressRule _chinaRule = null;


        public YPChinaNotification():base()
        {

        }

        public YPChinaNotification(IYHLog yhLog) : base(yhLog)
        {
            if (yhLog == null)
            {
                throw new ArgumentNullException("日志接口不能为空");
            }
        }

        //记录日志


        public override void Send(INotificationTarget target, INotificationBody body)
        {
            HttpParameter paramter = new HttpParameter();

            paramter.SetPostParameter(body);

            ISerializableObject serializableObject = new YPSerailizable(base.ApiKey, target, body);

            var httpRequest = new HttpWebRequestUtility(base.BaseUri + SINGLEURI, paramter, HttpMethod.POST, HttpContentType.JSON, serializableObject);

            httpRequest.AfterRequest += this.HttpRequest_AfterRequest;

            httpRequest.Request();
        }

        public void SendAsych(INotificationTarget target, INotificationBody body)
        {
            Task.Run(()=> {
                Send(target,body);
              });
        }
        public void BatchSend(IList<INotificationTarget> targets, INotificationBody body)
        {
            IList<INotificationTarget> successTarget = new List<INotificationTarget>();


            foreach (var item in targets)
            {
                try
                {
                    item.Validate(_chinaRule);

                    successTarget.Add(item);
                }
                catch (NotificationAddressInvalidException ex)
                {

                }
            }

            HttpParameter paramter = new HttpParameter();

            paramter.SetPostParameter(body);

            ISerializableObject serializableObject = new YPSerailizable(base.ApiKey, successTarget, body);

            var httpRequest = new HttpWebRequestUtility(base.BaseUri + SINGLEURI, paramter, HttpMethod.POST,
                HttpContentType.JSON, serializableObject);

            httpRequest.Request();


        }

        public void BatchSendAsych(IList<INotificationTarget> targets, INotificationBody body)
        {
            Task.Run(() => {

                BatchSend(targets,body);

            });
        }

        public void MultiSend(IList<INotificationContent> contents)
        {
            IList<INotificationContent> successContents = new List<INotificationContent>();

            foreach (var item in contents)
            {
                try
                {
                    item.Target.Validate(this._chinaRule);

                    successContents.Add(item);
                }
                catch (NotificationAddressInvalidException ex)
                {
                }
            }

            HttpParameter paramter = new HttpParameter();

            paramter.SetPostParameter(successContents);

            ISerializableObject serializableObject = new YPSerailizable(base.ApiKey, successContents);

            var httpRequest = new HttpWebRequestUtility(base.BaseUri + SINGLEURI, paramter, HttpMethod.POST,
               HttpContentType.JSON, serializableObject);

            httpRequest.Request();
        }

        public void MultiSendAsych(IList<INotificationContent> contents)
        {
            Task.Run(() =>
            {
                 MultiSend(contents);
            });
        }
    }
}
