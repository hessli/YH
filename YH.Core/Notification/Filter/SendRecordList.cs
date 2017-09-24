using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification.Filter
{
   public class SendRecordList
    {
        object _synch = new object();

        private IList<SendRecord> _sendRecord;
        public SendRecordList()
        {
            _sendRecord = new List<SendRecord>();
        }
        public void Add(SendRecord record)
        {
            lock (_synch)
            {
                _sendRecord.Add(record);

                _count++;
                
            }
        }

        private int _count;
        public int Count
        {
            get {

                return _count;
            }

        }

        public IList<SendRecord> SendRecords
        {
            get {

                lock (_synch)
                {

                    return _sendRecord;
                }
            }
        }

        public void Remove()
        {
            lock (_synch)
            {
                _sendRecord = _sendRecord.OrderBy(x => x.SendTime).ToList();

                var count = _sendRecord.Count;

                var index = count - 1;

                while (count > 1)
                {
                    _sendRecord.RemoveAt(index);

                    count--;
                }
            }
        }

         
    }
}
