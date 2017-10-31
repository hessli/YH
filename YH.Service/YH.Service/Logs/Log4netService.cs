using System;
using log4net;
using YH.Core.Logs;

namespace YH.Service.Logs
{
    public class Log4netService : IYHLog
    {
        ILog _log = null;  

        public event NotificationHandler Notification;

        public Log4netService(ILog log)
        {
            _log = log;
        }
        public void Error(ILogParatmer paramter)
        {
            _log.Error(paramter.Formart());

            this.OnNotifaction(paramter);
        }

        public void Error(string text,Exception ex )
        {

            _log.Error(text, ex);
        }
        public void ErrorAsych(ILogParatmer paramter)
        {
        
            _log.Error(paramter.Formart());

            this.OnNotifaction(paramter);
        }

        public void Info(ILogParatmer paramter)
        {
         
            _log.Info(paramter.Formart());

            this.OnNotifaction(paramter);
        }

        public void InfoAsych(ILogParatmer paramter)
        {
            _log.Info(paramter.Formart());

            this.OnNotifaction(paramter);
        }
        public void Info(string text)
        {
            _log.Info(text);

            this.Notification(this,text);
        }

        private void OnNotifaction(object value)
        {
            if (this.Notification != null)
            {
                this.Notification(this,value);
            }
        }

        public void Error(string text)
        {
            _log.Error(text);


            OnNotifaction(text);
        }
    }
}
