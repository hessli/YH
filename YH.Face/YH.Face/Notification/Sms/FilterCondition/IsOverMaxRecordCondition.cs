using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YH.Face.Notification.Sms.FilterCondition
{
    public class IsOverMaxRecordCondition : ISmsCondition
    {
        private int _maxRecord=0;
        private SendRecordList _sendRcordList;
        public IsOverMaxRecordCondition(SendRecordList sendRcordList,int maxRcord= int.MaxValue)
        {

            _sendRcordList = sendRcordList;

            _maxRecord = maxRcord;
        }
        public bool CanSend()
        {
            return !(_sendRcordList.Count > _maxRecord);

        }

     
    }
}
