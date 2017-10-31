namespace YH.Core.Notification.Sms
{
    public class SmsNotificationTarget : INotificationTarget
    {
        public SmsNotificationTarget(string areaCode, string mobileNumber)
        {
            _areaCode = areaCode;

            _address = mobileNumber;
        }

        private string _address;
        public string Address
        {
            get
            {
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

        public void Validate(INotificationAddressRule rule)
        {
              rule.Validate(this.Address);
        }
    }
}
