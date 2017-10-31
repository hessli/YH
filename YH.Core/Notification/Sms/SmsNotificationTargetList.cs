using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification.Sms
{
   public class SmsNotificationTargetList
    {
        private HashSet<string> _target;

        private Dictionary<string, IList<SmsNotificationTarget>> _dic;
        public SmsNotificationTargetList()
        {
            _dic = new Dictionary<string, IList<SmsNotificationTarget>>();

            _target = new HashSet<string>();

        }

        public int Count { get; private set; }

        public string[] AreaCodes
        {

            get
            {
                return _dic.Keys.ToArray();
            }
        }

        public  void Add(SmsNotificationTarget content)
        {

            IList<SmsNotificationTarget> contents = null;
           
            if (!this._dic.TryGetValue(content.AreaCode, out contents))
            {
                contents = new List<SmsNotificationTarget>();

                _dic.Add(content.AreaCode, contents);
            }
            if (!_target.Contains(content.AreaCode))
            {
                _target.Add(content.Address);

                contents.Add(content);

                Count++;
            }
        }

        public IList<SmsNotificationTarget> this[string areacode]
        {
            get
            {

                if (_dic.ContainsKey(areacode))
                {
                    return _dic[areacode];
                }
                return null;
            }
        }

        public  void Clear()
        {
            _dic.Clear();

            _target.Clear();

            Count = 0;
        }

        public  IEnumerator<SmsNotificationTarget> GetEnumerator()
        {
            foreach (var item in _dic)
            {
                foreach (var subItem in item.Value)
                {
                    yield return subItem;
                }
            }
        }
    }
}
