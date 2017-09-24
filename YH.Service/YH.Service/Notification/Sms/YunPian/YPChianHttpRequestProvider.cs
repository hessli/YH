using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Http;
using YH.Core.Notification;
using YH.Core.Notification.Rules;
using YH.Core.Serializable;

namespace YH.Service.Notification.Sms.YunPian
{

    /// <summary>
    /// 国内短信发送
    /// </summary>
    public class YPChianHttpRequestProvider: ISendHttpRequest
    {
        private INotificationAddressRule _chinaRule = null;

        private string _apikey;

        private string _baseUri=string.Empty;

        static readonly string SINGLEURI = "v2/sms/single_send.json";

        static readonly string BATCHURI = "/v2/sms/batch_send.json";

        static readonly string MULTIURI = "v2/sms/multi_send.json";

        static YPChianHttpRequestProvider()
        {

            var singleUri= System.Configuration.ConfigurationManager.AppSettings["YpSingleUri"];

            if (!string.IsNullOrWhiteSpace(singleUri))
            {
                SINGLEURI = singleUri;
            }
            var batchUri = System.Configuration.ConfigurationManager.AppSettings["YpBatchUri"];

            if (!string.IsNullOrWhiteSpace(batchUri))
            {
                BATCHURI = batchUri;
            }
            var multiUri = System.Configuration.ConfigurationManager.AppSettings["YpMultiUri"];

            if (!string.IsNullOrWhiteSpace(multiUri))
            {
                MULTIURI = multiUri;
            }
        }
        
        public YPChianHttpRequestProvider(string baseUri,string apikey)
        {
            _chinaRule = new ChinaMobileRule();

            _baseUri = baseUri;

            _apikey = apikey;
        }


        public HttpWebRequestUtility SingleSendHttpRequest(INotificationTarget target, INotificationBody body)
        {

            target.Validate(_chinaRule);

            HttpParameter paramter = new HttpParameter();
            
            paramter.SetPostParameter(body);

            ISerializableObject serializableObject = new YPSerailizable(_apikey,target,body);

            var  httpRequest = new HttpWebRequestUtility(_baseUri+SINGLEURI, paramter, HttpMethod.POST, HttpContentType.JSON,serializableObject);

           return httpRequest;
        }


        public IList<HttpWebRequestUtility> BatchSendHttpRequest(NotificationTargetList targets, INotificationBody body)
        {
            IList<INotificationTarget> successTarget = new List<INotificationTarget>();

            IList<HttpWebRequestUtility> httpRequests = new List<HttpWebRequestUtility>();

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

            ISerializableObject serializableObject = new YPSerailizable(_apikey, successTarget, body);

            var httpRequest = new HttpWebRequestUtility(_baseUri + SINGLEURI, paramter,HttpMethod.POST,
                HttpContentType.JSON,serializableObject);

            httpRequests.Add(httpRequest);

            return httpRequests;

        }

        public IList<HttpWebRequestUtility> MultiSendHttpRequest(NotificationContentList contents)
        {
            IList<INotificationContent> successContents = new List<INotificationContent>();

            IList<HttpWebRequestUtility> httpRequests = new List<HttpWebRequestUtility>();

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

            ISerializableObject serializableObject = new YPSerailizable(_apikey, successContents);

            var httpRequest = new HttpWebRequestUtility(_baseUri + SINGLEURI, paramter, HttpMethod.POST,
               HttpContentType.JSON, serializableObject);

            httpRequests.Add(httpRequest);

            return httpRequests;
        }
    }
}
