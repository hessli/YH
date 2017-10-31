using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Logs;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms.YunPian
{
    public abstract class YPBaseSmsNotification
    {
        private string _apikey = ConfigurationManager.AppSettings["YunPianApikey"];

        protected string ApiKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_apikey))
                {
                    return _apikey = "wxb";
                }
                return _apikey;
            }
        }

        private string _baseUri = ConfigurationManager.AppSettings["YunPianBaseUri"];

        protected string BaseUri
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

        protected IYHLog YhLog
        {
            get;
            private set;
        }

        public YPBaseSmsNotification()
        {

        }

        public YPBaseSmsNotification(IYHLog yhLog) : this()
        {
            YhLog = yhLog;
        }

        public abstract void Send(INotificationTarget target, INotificationBody body);

        protected void HttpRequest_AfterRequest(object sender, EventArgs e)
        {
            var reqeustArgs = (HttpAfterArgs)e;

            if (YhLog != null)
            {
                if (reqeustArgs.IsSuccess == 0)
                {
                    YhLog.ErrorAsych(reqeustArgs.ToRequestLog());
                }
                else
                {
                    YhLog.InfoAsych(reqeustArgs.ToRequestLog(Core.Comm.LogLevel.严重));
                }
            }
        }

    }
}
