using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Face.Notification.Sms.FilterCondition
{
   public class RequestFrequentlyException:Exception
    {
        public RequestFrequentlyException() : base("请求太过频繁")
        { }


        public RequestFrequentlyException(string msg) : base(msg)
        {

        }
    }
}
