using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;
using YH.Service.Notification.Sms.YunPian;

namespace YH.Service.Notification.Sms
{
   public class ISendHttpRequestProcessor
    {

        ConcurrentDictionary<string, IList<ISendHttpRequest>> dic;


        static ISendHttpRequestProcessor _processor;

        public static ISendHttpRequestProcessor Instance {
            get {
                return _processor;
            }

        }

        static ISendHttpRequestProcessor()
        {
            _processor = new ISendHttpRequestProcessor();
        }

        ISendHttpRequestProcessor()
        {
            dic = new ConcurrentDictionary<string,  IList<ISendHttpRequest>>();
        }
        public void Regist()
        {
            var chianRequest = new List<ISendHttpRequest>();

            var foreginRequest = new List<ISendHttpRequest>();

            chianRequest.Add(new YunPian.YPChianHttpRequestProvider(YunPianNotification.BASEURI, YunPianNotification.APIKEY));


        }
        public IList<ISendHttpRequest> Get(string areaCode)
        {
            IList<ISendHttpRequest> sendHttpRequest=null;

            dic.TryGetValue(areaCode, out sendHttpRequest);

            return sendHttpRequest;
        }
        
    }
}
