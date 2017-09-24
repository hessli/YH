using System;

namespace YH.Core.Comm
{
    public enum  LogType
    {
         调用第三方接口日志=1,
         系统内部日志=2
    }

    public enum SqlProviderType
    {
         SQLServer=1,
         Mysql=2
    }


    public enum LogLevel
    {
         严重=1,
         普通=2,
         默认=3

    }

    [Flags]
    public enum NotificationType
    {
         短信=1,
         邮件=2,
         全部= 短信 & 邮件
    }
}
