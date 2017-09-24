using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Core.Notification
{
    public interface INotificationAddressRule
    {
        void Validate(string address); 
    }
}
