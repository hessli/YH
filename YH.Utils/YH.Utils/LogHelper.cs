using log4net;
using System.Text;
using YH.Service.Logs;

namespace YH.Utils
{
    public  class LogHelper
    {
        static Log4netService LOGGER_REQUEST = new Log4netService(LogManager.GetLogger("RequestLog"));
       
        static Log4netService LOGGER_RESPONSE = new Log4netService(LogManager.GetLogger("ResponseLog"));
        enum LogType
        {
            Request,
            Response
        }

        static void AppendFormat(StringBuilder sb, string format, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                sb.Append(format);
            }
            else
            {
                sb.AppendFormat(format, args);
            }
            sb.AppendLine("\r\n-------------------------------------------------------------------------------------");
        }

        static void WriteLog(LogType logType, string message, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------");

            AppendFormat(sb, message, args);

            sb.AppendLine("\r\n-------------------------------------------------------------------------------------");

            sb.AppendLine("****************************************End******************************************\r\n");

            if (logType == LogType.Request)
            {
                LOGGER_REQUEST.Error(sb.ToString());
            }
            else
            {
                LOGGER_RESPONSE.Error(sb.ToString());
            }
        }
        #region Resquest

        public static void WriteRequestLog(string message)
        {
            WriteRequestLog(message, null);
        }

        public static void WriteRequestLog(string message, params object[] args)
        {
            WriteLog(LogType.Request, message, args);
        }

        #endregion


        #region Response

        public static void WriteResponseLog(string message)
        {
            WriteResponseLog(message, null);
        }

        public static void WriteResponseLog(string message, params object[] args)
        {
            WriteLog(LogType.Response, message, args);
        }

        #endregion
    }
}
