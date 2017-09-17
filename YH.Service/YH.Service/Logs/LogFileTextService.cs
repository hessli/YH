using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.File;
using YH.Core.Logs;

namespace YH.Service.Logs
{
    public class LogFileTextService : IYHLog
    {
        private string _fileName;

        private FileUtility _fileUtility;

        static object Synch = new object();

        public event NotificationHandler Notification;

        public LogFileTextService()
        {
            _fileName = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("YYMMdd");
            _fileUtility = new FileUtility(_fileName);
        }
        public LogFileTextService(string fileName)
        {
            _fileName = fileName;

            _fileUtility = new FileUtility(_fileName);
        }

        public void Error(ILogParatmer paramter)
        {
            lock (Synch)
            {

                var text = paramter.Formart();

                var builder = new StringBuilder();

                builder.AppendFormat("-------------{0}:错误信息------------------", DateTime.Now);

                builder.AppendLine(text);

                builder.AppendFormat("--------------------错误日志结束-----------------------");


                _fileUtility.AppendLine(builder.ToString());


                OnNotification(text);
            }
        }
        private void OnNotification(object value)
        {

            if (this.Notification != null)
            {
                this.Notification(this, value);
            }
        }
        public void ErrorAsych(ILogParatmer paramter)
        {
            lock (Synch)
            {

                var text = paramter.Formart();

                var builder = new StringBuilder();

                builder.AppendFormat("-------------{0}:异步写错误信息------------------", DateTime.Now);

                builder.AppendLine(text);

                builder.AppendFormat("--------------------异步写错误日志结束-----------------------");

                OnNotification(text);
                try
                {
                    _fileUtility.AppendLineAsych(builder.ToString());
                }
                catch (AggregateException ex)
                {
                    var e = ex;
                }
            }
        }

        public void Info(ILogParatmer paramter)
        {
            lock (Synch)
            {
                var text = paramter.Formart();

                var builder = new StringBuilder();

                builder.AppendFormat("-------------{0}:日志信息------------------", DateTime.Now);

                builder.AppendLine(text);

                builder.AppendFormat("--------------------日志结束-----------------------");


                _fileUtility.AppendLine(builder.ToString());

                OnNotification(text);
            }
        }

        public void InfoAsych(ILogParatmer paramter)
        {
            lock (Synch)
            {
                var text = paramter.Formart();

                var builder = new StringBuilder();

                builder.AppendFormat("-------------{0}:异步日志信息------------------", DateTime.Now);

                builder.AppendLine(text);

                builder.AppendFormat("--------------------异步日志结束-----------------------");
                try
                {
                    _fileUtility.AppendLineAsych(builder.ToString());
                } catch (AggregateException ex)
                {
                    var e = ex;
                }
                OnNotification(text);
            }
        }
    }
}
