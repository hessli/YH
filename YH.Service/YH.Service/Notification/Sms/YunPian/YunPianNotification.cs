using System;
using YH.Core.Http;
using YH.Core.Logs;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YunPianNotification : INotification
    {

        static string _baseUri = System.Configuration.ConfigurationManager.AppSettings["YunPianBaseUri"];
        public static string BASEURI
        {
            get
            {

                if (string.IsNullOrWhiteSpace(_baseUri))
                {
                    _baseUri = "http://sms.yunpian.com";
                }

                return _baseUri;
            }
        }

        static string _apiKey = System.Configuration.ConfigurationManager.AppSettings["YunPianApikey"];
        public static string APIKEY
        {
            get
            {
                return _apiKey;
            }
        }

        private HttpWebRequestUtility _httpRequest;

        private IYHLog _yhLog;

        private ISendHttpRequest _sendHttpRequest;

        public YunPianNotification()
        {

        }
        public YunPianNotification(IYHLog yhLog)
        {
            _yhLog = yhLog;

            if (_yhLog == null)
            {
                throw new ArgumentNullException("日志接口不能为空");
            }
        }

        //记录日志
        private void HttpRequest_AfterRequest(object sender, EventArgs e)
        {
            var reqeustArgs = (HttpAfterArgs)e;

            if (_yhLog != null)
            {
                if (reqeustArgs.IsSuccess == 0)
                {
                    _yhLog.ErrorAsych(reqeustArgs.ToRequestLog());
                }
                else
                {
                    _yhLog.InfoAsych(reqeustArgs.ToRequestLog(Core.Comm.LogLevel.严重));
                }
            }
        }

        public void Send(INotificationTarget target, INotificationBody body)
        {
            var  yptarget=(YPNotificationTarget)target;

            var httpRequests=ISendHttpRequestProcessor.Instance.Get(yptarget.AreaCode);

            foreach (var item in httpRequests)
            {
                try
                {
                    var httpRequest = item.SingleSendHttpRequest(target, body);

                    httpRequest.Request();

                    httpRequest.AfterRequest += HttpRequest_AfterRequest;

                    break;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        public void SendAsych(INotificationTarget target, INotificationBody body)
        {

             
        }

        public void BatchSend(NotificationTargetList targets, INotificationBody body)
        {
            foreach (var item in targets)
            {
                     
            }
        }

        public void BatchSendAsych(NotificationTargetList targets, INotificationBody body)
        {
            throw new NotImplementedException();
        }

        public void MultiSend(NotificationContentList contents)
        {
            var smsContents = ((SmsNotificationContentList)contents);

            foreach (var item in smsContents.AreaCodes)
            {
                var sendHttpProcessor = ISendHttpRequestProcessor.Instance.Get(item);
                if (sendHttpProcessor == null)
                {
                    throw new NotSupportedException("不支持的地区");
                }
                var content = smsContents[item];

                foreach (var subItem in sendHttpProcessor)
                {
                    var requests=subItem.MultiSendHttpRequest(content);

                    foreach (var requestItem in requests)
                    {   
                        requestItem.AfterRequest += HttpRequest_AfterRequest;
                        try
                        {
                            requestItem.Request();

                            break;
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }
        }

        public void MultiSendAsych(NotificationContentList contents)
        {
            throw new NotImplementedException();
        }
    }
}
