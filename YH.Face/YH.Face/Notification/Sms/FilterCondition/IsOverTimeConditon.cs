using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Time;
namespace YH.Face.Notification.Sms.FilterCondition
{
    public class IsOverTimeConditon : ISmsCondition
    {

        private int _interval = 0;
        private SendRecordList _sendRcordList;
        public IsOverTimeConditon(SendRecordList sendRcordList,int interval)
        {

            _interval = interval;

            _sendRcordList = sendRcordList;
        }
        public bool CanSend()
        {
             return TimeHelper.DateTimeMsToLong(DateTime.Now) - _sendRcordList.SendTime > _interval;
        }
    }
}
