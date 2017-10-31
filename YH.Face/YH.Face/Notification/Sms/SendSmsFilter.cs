using System;
using YH.Face.Notification.Sms.FilterCondition;
using YH.Service.Notification.Sms;
using System.Time;
namespace YH.Face.Notification.Sms
{

    public class SendSmsFilter
    {

        private static SendSmsFilter _instance;
        public static SendSmsFilter Instance {

            get {

                return _instance;
            }
        }
        static SendSmsFilter()
        {
            _instance = new Sms.SendSmsFilter();

        }
        private SendSmsFilter()
        {
            _sendRcordList = new Sms.SendRecordList();
        }

        private SendRecordList _sendRcordList=null;
        public void Filter(string mobile ,string text)
        {
            //A)单位时间内不能超出最大记录数
            ISmsCondition isOverMax = new IsOverMaxRecordCondition(_sendRcordList, 1000);


            //B)不同的场景记录数大小不同,单位时间内相同场景不能超过最大数
            ISmsCondition isOverTime = new IsOverTimeConditon(_sendRcordList, 30000);

            if (!isOverTime.CanSend() || !isOverTime.CanSend())
            {
                throw new RequestFrequentlyException();
            }

            ISmsCondition isRepeat = new IsRepeartContentCondition(_sendRcordList, mobile, text, 30000);

            if (!isRepeat.CanSend())
            {
                throw new RequestFrequentlyException(string.Format("电话号码{0}发送太过频繁", mobile));
            }
        }


        public void Filter(SmsNotificationContentList contents)
        {

            ISmsCondition isOverMax = new IsOverMaxRecordCondition(_sendRcordList, 1000);

            ISmsCondition isOverTime = new IsOverTimeConditon(_sendRcordList,3);

            if (!isOverTime.CanSend() || !isOverTime.CanSend())
            {
                throw new RequestFrequentlyException();
            }

            foreach (var item in contents)
            {
                ISmsCondition isRepeat = new IsRepeartContentCondition(_sendRcordList,item.Target.Address,item.Body.Content,3);

                if (!isRepeat.CanSend())
                {
                    throw new RequestFrequentlyException(string.Format("电话号码{0}发送太过频繁",item.Target.Address));
                }
            }
        }

        public void Update(string mobile, string text)
        {
            _sendRcordList.Update(new SendRecord(TimeHelper.DateTimeMsToLong(DateTime.Now), mobile, text), 3);
        }


        public void Update(SmsNotificationContentList contents)
        {
            foreach (var item in contents)
            {
                Update( item.Target.Address, item.Body.Content);
            }
        }
    }
}
