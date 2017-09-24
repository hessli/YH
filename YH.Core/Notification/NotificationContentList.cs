using System.Collections.Generic;
namespace YH.Core.Notification
{
    public abstract  class NotificationContentList
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="content"></param>
        public abstract void Add(INotificationContent content);
        /// <summary>
        /// 删除
        /// </summary>
        public abstract void Clear();

        public abstract IEnumerator<INotificationContent> GetEnumerator();

        public int Count { get; protected set; }

        //private IList<INotificationContent> _contents;

        //private HashSet<string> _target;

        //public NotificationContentList()
        //{
        //    _contents = new List<INotificationContent>();

        //    _target = new HashSet<string>();
        //}

        //public NotificationContentList(IList<INotificationContent> contents):this()
        //{
        //    if (contents == null)
        //        throw new ArgumentNullException("通知信息不可为空");

        //    _contents = contents;
        //}
        //public  virtual void Add(INotificationContent content)
        //{
        //    _contents.Add(content);
        //}

        //public IList<INotificationContent> Contents {

        //    get {
        //        return _contents;
        //    }
        //}

        //public int Count
        //{
        //    get
        //    {
        //        return _contents.Count;
        //    }
        //}

        //public IEnumerator<INotificationContent> GetEnumerator()
        //{
        //    foreach (var item in this._contents)
        //    {
        //        yield return item;
        //    }
        //}

        //public virtual void Clear()
        //{
        //    _contents.Clear();

        //    _target.Clear();
        //}

        //public virtual void HashAdd(INotificationContent content)
        //{
        //    if (_target.Contains(content.Target.Address))
        //        return;

        //    _target.Add(content.Target.Address);

        //    _contents.Add(content);
        //}
    }
}
