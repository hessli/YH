using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using YH.Core.Comm;

namespace YH.Core.Notification.Filter
{
    public abstract  class BaseNotificationFilter
    {
        static readonly ConcurrentDictionary<NotificationType,IList<SendRecord>> _dic=null;

        private NotificationType _type;

        private INotificationFilter _notificationFilter;
        static BaseNotificationFilter()
        {
            _dic= new ConcurrentDictionary<NotificationType, IList<SendRecord>>();
        }

        public BaseNotificationFilter(INotificationFilter filter, NotificationType type )
        {

            _type = type;

            _notificationFilter = filter;
        }

        public IList<INotificationContent> TryFilter(NotificationContentList contentList,out IList<INotificationContent> errorContent)
        {
            errorContent = new List<INotificationContent>();

            IList<INotificationContent> content = new List<INotificationContent>();

            foreach (var item in contentList)
            {
                if (!IsFilter(item) )
                {
                    content.Add(item);
                }
                else
                {
                    errorContent.Add(item);
                }
            }
            return content;
        }

        /// <summary>
        /// 是否需要过滤
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool IsFilter(INotificationContent content)
        {
           return   _notificationFilter.CompareTo(content)>0;
        }
        private  void UpdateFilter()
        {
            _dic.TryRemove()
        }

       
    }
    
}
