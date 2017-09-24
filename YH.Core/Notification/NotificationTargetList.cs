using System;
using System.Collections.Generic;

namespace YH.Core.Notification
{
    public class NotificationTargetList
    {
        private IList<INotificationTarget> _targets;
        private HashSet<string> _hash;

        private int MaxLength=Int32.MaxValue;
        public NotificationTargetList(int maxLength= Int32.MaxValue)
        {
            _targets = new List<INotificationTarget>();

            _hash = new HashSet<string>();

            MaxLength = maxLength;
        }

        public NotificationTargetList(IList<INotificationTarget> targets,int maxLength= Int32.MaxValue) : this(maxLength)
        {

            if (targets == null)

                throw new ArgumentNullException("地址集合不能为空");
            _targets = targets;
        }

        public void Add(INotificationTarget target)
        {

            if (this.Cout > MaxLength)
                throw new ArgumentOutOfRangeException("超出最大发送值");

            target.Validate();

            _targets.Add(target);
        }

        public void HashAdd(INotificationTarget target)
        {
            if (_hash.Contains(target.Address))
                return;

            if (this.Cout > MaxLength)
                throw new ArgumentOutOfRangeException("超出最大发送值");

            _hash.Add(target.Address);

            _targets.Add(target);
        }

        public IEnumerator<INotificationTarget> GetEnumerator()
        {
            foreach (var item in this._targets)
            {
                yield return item;
            }
        }

        public int Cout
        {
            get
            {

                return _targets.Count;
            }
        }
        public void Clear()
        {
            _targets.Clear();

            _hash.Clear();
        }
    }
}
