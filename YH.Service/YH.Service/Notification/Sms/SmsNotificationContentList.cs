using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;

namespace YH.Service.Notification.Sms
{
    public class SmsNotificationContentList : NotificationContentList
    {
        private HashSet<string> _target;
   
        private Dictionary<string, IList<INotificationContent>> _dic;
        public SmsNotificationContentList()
        {
            _dic = new Dictionary<string, IList<INotificationContent>>();

             _target = new HashSet<string>();
            
        }
        public string[] AreaCodes {

            get {
                return _dic.Keys.ToArray();
            }
        }

        public override void Add(INotificationContent content)
        {
            var target = (SmsBaseNotificationTarget)content.Target;

            IList<INotificationContent> contents=null;
            if (!this._dic.TryGetValue(target.AreaCode, out contents))
            {
                contents = new List<INotificationContent>();
                 
                _dic.Add(target.AreaCode, contents);    
            }
            if (!_target.Contains(target.Address))
            {
                _target.Add(target.Address);

                contents.Add(content);

                base.Count++;
            }
        }

        public IList<INotificationContent> this[string areacode]
        {
            get {

                if (_dic.ContainsKey(areacode))
                {
                    return _dic[areacode];
                }
                return null;
            }
        }

        public override void Clear()
        {
            _dic.Clear();

            _target.Clear();

            base.Count = 0;
        }

        public override IEnumerator<INotificationContent> GetEnumerator()
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
