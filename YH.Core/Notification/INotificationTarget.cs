using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification
{
   public interface INotificationTarget
    {
        string Address { get;  }

         /// <summary>
         /// 验证地址有效性
         /// </summary>
       void Validate(INotificationAddressRule rule);


    }
}
