using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Face.Notification.Sms.FilterCondition
{
    public class IsRepeartContentCondition : ISmsCondition
    {

        private SendRecordList _recordList;

        private string _telNumber;

        private string _content;

        private int _interval=0;

        public IsRepeartContentCondition(SendRecordList rcordList,
            string telNumber,string content,
            int interval)
        {

            _recordList = rcordList;

            _telNumber = telNumber;

            _content=content;

            _interval = interval;
        }
        public bool CanSend()
        {
            var record= _recordList[_telNumber];

            if (record == null)
                return true;

           return ! (record.DiffrenceTime() < _interval &&
             record.Compare(_content)
             );
          
        }
    }
}
