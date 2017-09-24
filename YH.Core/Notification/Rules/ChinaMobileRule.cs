using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YH.Core.Notification.Rules
{
    public class ChinaMobileRule : INotificationAddressRule
    {
        static readonly string parrent = @"^(0|86|17951)?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$";
        public void Validate(string address)
        {
          var isSuccs= Regex.IsMatch(address,parrent);

            if (!isSuccs)
            {
                throw new NotificationAddressInvalidException("手机号格式错误");
            }
        }
    }
}
