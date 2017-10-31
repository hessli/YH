using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Core.Notification;
using YH.Core.Serializable;

namespace YH.Service.Notification.Sms.YunPian
{
    public class YPSerailizable : ISerializableObject
    {
        public YPSerailizable()
        {

        }
        private string _apiKey=string.Empty;

        private SmsSendType _sendType;

        private string _paramterTemp="apikey={0}&mobile={1}&text={2}";

        private INotificationTarget _target;

        private IEnumerable<INotificationTarget> _targets;

        private INotificationBody _body;

        private IEnumerable<INotificationContent> _contents;

         YPSerailizable(string apiKey, SmsSendType sendType)
        {
            _apiKey = apiKey;

            _sendType = sendType;
        }
        public YPSerailizable(string apikey,INotificationTarget target, INotificationBody body):this(apikey,SmsSendType.单条)
        {
            _target = target;

            _body = body;
        }

        public YPSerailizable(string apikey, IEnumerable<INotificationTarget> targets,
            INotificationBody body):this(apikey,SmsSendType.不同号码相同内容)
        {
            _targets = targets;

            _body = body;
        }

        public YPSerailizable(string apikey, IEnumerable<INotificationContent> contents):this(apikey,SmsSendType.不同内容不同号码)
        {
         
            _contents = contents;
        }

        public T JsonDeserializeObject<T>(string jsonStr) where T : class, new()
        {
            NewtonSerailizable newtownSerializable = new NewtonSerailizable();

            return   newtownSerializable.JsonDeserializeObject<T>(jsonStr);
        }

        public string JsonSerializableObject<T>(T obj)
        {
            var paramters = string.Empty;
            switch (_sendType)
            {
                case SmsSendType.单条:
                      
                    paramters = JsonSerializableSingle();

                    break;

                case SmsSendType.不同内容不同号码:
                     
                    paramters= JsonSerializableMuli();

                    break;
                case SmsSendType.不同号码相同内容:

                    paramters= JsonSerializableBatch();

                    break;
            }
            return paramters;
        }

        /// <summary>
        /// 单条记录
        /// </summary>
        /// <returns></returns>
        string JsonSerializableSingle()
        {
           return   string.Format(_paramterTemp,_apiKey,_target.Address, _body.Content);
        }

        string JsonSerializableBatch()
        {
            var mobiles = new StringBuilder();
            foreach (var item in this._targets)
            {
                mobiles.Append(item.Address);
                mobiles.Append(",");
            }

           var paramters = string.Format(_paramterTemp, _apiKey, mobiles.ToString().TrimEnd(','), _body.Content);

            return paramters;
        }

        /// <summary>
        /// 不同号码不同内容
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string  JsonSerializableMuli() 
        {
            var paramters = string.Empty;

            var mobiles = new StringBuilder();

            var contents = new StringBuilder();

            foreach (var item in _contents)
            {
                mobiles.Append(item.Target.Address);
                mobiles.Append(",");
                contents.Append(item.Body.Content);
                contents.Append(",");
            }

            paramters =string.Format(_paramterTemp, _apiKey,mobiles.ToString().TrimEnd(','),contents.ToString().TrimEnd(','));

            return paramters;

        }

    }
}
