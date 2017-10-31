using System.Collections.Generic;
using System.Linq;
using YH.Core.Notification;
using YH.Core.Notification.Sms;

namespace YH.Service.Notification.Sms
{
    public class SmsNotificationContentList 
    {
        private HashSet<string> _target;
   
        private Dictionary<string, IList<SmsNotificationContent>> _dic;

        public int Count { get; private set; }

        public SmsNotificationContentList()
        {
            _dic = new Dictionary<string, IList<SmsNotificationContent>>();

             _target = new HashSet<string>();
        }
        public string[] AreaCodes {

            get {
                return _dic.Keys.ToArray();
            }
        }
        public  void Add(INotificationContent content)
        {

            IList<SmsNotificationContent> contents=null;
            var smsTraget= ((SmsNotificationTarget)content.Target);

            if (!this._dic.TryGetValue(smsTraget.AreaCode, out contents))
            {
                contents = new List<SmsNotificationContent>();

                _dic.Add(smsTraget.AreaCode, contents);
            }
            if (!_target.Contains(smsTraget.Address))
            {
                _target.Add(smsTraget.Address);

                contents.Add((SmsNotificationContent)content);

                 Count++;
            }
        }

        public IList<SmsNotificationContent> this[string areacode]
        {
            get {

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

        public  IEnumerator<INotificationContent> GetEnumerator()
        {
            foreach (var item in _dic)
            {
                foreach (var subItem in item.Value)
                {
                    yield return  subItem;
                }
            }
        }

     
    }
}
