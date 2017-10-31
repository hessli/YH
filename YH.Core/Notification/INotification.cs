using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification
{
   public interface INotification
    { 
        /// <summary>
        /// 单条发送
        /// </summary>
        /// <param name="target"></param>
        /// <param name="body"></param>
        void Send(INotificationTarget  target, INotificationBody body);
        /// <summary>
        /// 单条异步发送
        /// </summary>
        /// <param name="target"></param>
        /// <param name="body"></param>
        void SendAsych(INotificationTarget target, INotificationBody body);
        /// <summary>
        /// 批量发送相同内容
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="body"></param>
        void BatchSend(IList<INotificationTarget> targets, INotificationBody body);
        /// <summary>
        /// 批量异步发送相同内容
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="body"></param>
        void BatchSendAsych(IList<INotificationTarget> targets, INotificationBody body);
        /// <summary>
        /// 批量发送不同内容
        /// </summary>
        /// <param name="contents"></param>
        void MultiSend(IList<INotificationContent> contents);
        /// <summary>
        /// 批量异步发送不同内容
        /// </summary>
        /// <param name="contents"></param>
        void MultiSendAsych(IList<INotificationContent> contents);
    }
}
