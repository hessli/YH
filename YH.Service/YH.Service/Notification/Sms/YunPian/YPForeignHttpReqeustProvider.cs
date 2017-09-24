using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Notification;
using YH.Core.Serializable;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPForeignHttpReqeustProvider : ISendHttpRequest
    {
        static readonly string SINGLEURI = "v2/sms/single_send.json";

        private string _apikey;

        private string _baseUri = string.Empty;
        static YPForeignHttpReqeustProvider()
        {
             var singleUri = System.Configuration.ConfigurationManager.AppSettings["YpSingleUri"];

            if (!string.IsNullOrWhiteSpace(singleUri))
            {
                SINGLEURI = singleUri;
            }
        }
        public YPForeignHttpReqeustProvider(string baseUri, string apikey)
        {
            _baseUri = baseUri;

            _apikey = apikey;
        }

        public IList<HttpWebRequestUtility> BatchSendHttpRequest(NotificationTargetList targets, INotificationBody body)
        {
            IList<HttpWebRequestUtility> requests = new List<HttpWebRequestUtility>();

            foreach (var item in targets)
            {
               var request= SingleSendHttpRequest(item,body);

                requests.Add(request);
            }

            return requests;
        }

        public IList<HttpWebRequestUtility> MultiSendHttpRequest(NotificationContentList contents)
        {
            IList<HttpWebRequestUtility> requests = new List<HttpWebRequestUtility>();
            foreach (var item in contents)
            {
                var request = SingleSendHttpRequest(item.Target, item.Body);

                requests.Add(request);
            }

            return requests;
        }

        public HttpWebRequestUtility SingleSendHttpRequest(INotificationTarget target, INotificationBody body)
        {
           
            HttpParameter paramter = new HttpParameter();

            paramter.SetPostParameter(body);

            ISerializableObject serializableObject = new YPSerailizable(_apikey, target, body);

            var httpRequest = new HttpWebRequestUtility(_baseUri + SINGLEURI, paramter, HttpMethod.POST, HttpContentType.JSON, serializableObject);

            return httpRequest;
        }
    }
}
