
using System;
using System.Collections.Generic;
using System.Time;

namespace YH.Face.Notification.Sms
{


    public class SendRecordList
    {

        object _synch = new object();
 
        private IList<SendRecord> recordList;

        public SendRecordList()
        {
            recordList = new List<SendRecord>();
        }
        public void Update(SendRecord record,int interval)
        {
            lock (_synch)
            {
                recordList.Insert(recordList.Count==0?0:recordList.Count-1,record);

                Remove(interval);

                //如果当前发送
                _count = recordList.Count;

                SendTime = TimeHelper.DateTimeMsToLong(DateTime.Now);
            }
        }

        private int _count;
        public int Count
        {
            get {

                return _count;
            }
        }
       
         public SendRecord this[string phoneNumber]
        {
            get {
                lock (_synch)
                {
                    foreach (var item in this.recordList)
                    {
                        if (item.PhoneNumber.Equals(item.PhoneNumber))
                        {
                            return item;
                        }
                    }

                    return null;
                }
            }
        }

        public long SendTime
        {
            get;
            private set;
        }

        private  void Remove(int interval)
        {
            lock (_synch)
            {
                var count = 0;

                var length = recordList.Count-1;

                while (count < length)
                {
                    if (recordList[count].DiffrenceTime() > interval)
                    {
                        recordList.RemoveAt(count);
                    }
                    else
                    {
                        break;
                    }
                    count++;
                }
            }
        }
         
    }
}
