using System;
using System.Time;
namespace YH.Face.Notification.Sms
{
    public class SendRecord
    {
        private long _sendTime;

        private string _phoneNumber;


        private string _content;

        public SendRecord(long sendTime, 
            
            string phoneNumber,
            string content)
        {
            _sendTime = sendTime;

            _phoneNumber = phoneNumber;

            _content = content;
        }
        public long SendTime
        {
            get
            {
                return _sendTime;
            }
        }


        public string Content
        {
            get {
                return _content;
            }

        }


        public long DiffrenceTime()
        {
           return  TimeHelper.DateTimeMsToLong(DateTime.Now) - this._sendTime;
        }

        public bool Compare(string content)
        {

            return  _content.Equals(content);
        }
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
        }
    }
}
