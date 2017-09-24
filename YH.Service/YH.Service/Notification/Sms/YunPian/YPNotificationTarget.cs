using YH.Core.Notification;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPNotificationTarget : SmsBaseNotificationTarget
    {
        public YPNotificationTarget(string areaCode, string phoneNumber):base(areaCode,phoneNumber)
        {
           
        }
       
        public override void Validate(INotificationAddressRule rule)
        {
            rule.Validate(this.Address);
        } 
    }
}
