using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms
{
    public abstract class SmsBaseNotificationTarget : INotificationTarget
    {
        public SmsBaseNotificationTarget(string areaCode,string mobileNumber)
        {
            _address = mobileNumber;

            _areaCode = areaCode;
        }

        private string _address;
        public string Address
        {
            get {
                return _address;
            }
        }

        private string _areaCode;
        public string AreaCode
        {
            get {

                return _areaCode;
            }
        }
        public abstract void Validate(INotificationAddressRule rule);
    }
}
