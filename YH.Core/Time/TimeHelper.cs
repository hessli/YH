using System.Collections.Generic;

namespace System.Time
{
    public class TimeHelper
    {

        static TimeHelper()
        {

            _dayOfWeekDic.Add(DayOfWeek.Monday, "周一");
            _dayOfWeekDic.Add(DayOfWeek.Tuesday, "周二");
            _dayOfWeekDic.Add(DayOfWeek.Wednesday, "周三");
            _dayOfWeekDic.Add(DayOfWeek.Thursday, "周四");
            _dayOfWeekDic.Add(DayOfWeek.Friday, "周五");
            _dayOfWeekDic.Add(DayOfWeek.Saturday, "周六");
            _dayOfWeekDic.Add(DayOfWeek.Sunday, "周日");
        }

        static DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

        private const long SecondValueAt1970 = 621355968000000000L;
        /// <summary>
        /// 根据自1970年以来的秒数来获取时间
        /// </summary>
        /// <param name="timeValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeOfSecondFrom1970(long timeValue)
        {
            DateTime dateTime = new DateTime((long)(timeValue * 10000 * 1000) + SecondValueAt1970);
            return dateTime;
        }

        public static DateTime? GetNullDateTimeOfSecondFrom1970(long timeValue)
        {

            if (timeValue == 0)
                return default(DateTime?);

            DateTime dateTime = new DateTime((long)(timeValue * 10000 * 1000) + SecondValueAt1970);
            return dateTime;
        }

        /// <summary>
        /// 根据自1970年以来的毫秒数来获取时间
        /// </summary>
        /// <param name="timeValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeOfMillisecondFrom1970(long timeValue)
        {
            DateTime dateTime = new DateTime((long)(timeValue * 10000) + SecondValueAt1970);
            return dateTime;
        }

        /// <summary>
        /// 获取自1970年以来的秒数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetSecondFrom1970(DateTime dateTime)
        {
            return (dateTime.Ticks - SecondValueAt1970) / 10000 / 1000;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static uint DateTimeToUInt(DateTime time)
        {
            return (uint)(time - StartTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTimeToInt(DateTime time)
        {
            return (int)(time - StartTime).TotalSeconds;
        }
        public static int DateTimeToInt(DateTime? time)
        {
            if (time == null) return 0;
            DateTime t = (DateTime)time;
            return (int)(t - StartTime).TotalSeconds;
        }

        public static long DateTimeToIntMs(DateTime time)
        {
            return (long)(time - StartTime).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeToLong(DateTime time)
        {
            return (long)(time - StartTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式根据ms毫秒数转换为ms时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeMsToLong(DateTime time)
        {
            return (long)(time - StartTime).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime时间格式根据ms毫秒数转换为ms时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTimeMsToLoginStr(DateTime time)
        {
            return (time - StartTime).TotalMilliseconds.ToString();
        }

        /// <summary>
        /// Unix时间戳格式转换为DateTime时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime UIntToDateTime(uint value)
        {
            return StartTime.AddSeconds(value);
        }

        /// <summary>
        /// Int时间戳格式转换为DateTime时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime IntToDateTime(int value)
        {
            return StartTime.AddSeconds(value);
        }

        public static DateTime LongToDateTime(long value)
        {
            return StartTime.AddSeconds(value);
        }

        /// <summary>
        /// 时间戳转年龄
        /// </summary>
        /// <param name="birthTimestamp">时间戳</param>
        /// <returns></returns>
        public static int TimestampToAge(int birthTimestamp)
        {
            var birthDate = IntToDateTime(birthTimestamp);
            return DateTimeToAge(birthDate);
        }

        /// <summary>
        /// 时间戳转年龄
        /// </summary>
        /// <param name="birthTimestamp">时间戳</param>
        /// <returns></returns>
        public static int TimestampToAge(long birthTimestamp)
        {
            var birthDate = LongToDateTime(birthTimestamp);
            return DateTimeToAge(birthDate);
        }


        /// <summary>
        /// 日期转年龄
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static int DateTimeToAge(DateTime birthDate)
        {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;
            return age;
        }

        static Dictionary<DayOfWeek, string> _dayOfWeekDic = new Dictionary<DayOfWeek, string>();

        public static string ToWeek(DateTime time)
        {

            string week = string.Empty;
            if (time != default(System.DateTime))
            {
                week = _dayOfWeekDic[time.DayOfWeek];
            }
            return week;
        }

        static Dictionary<string, DateTime> _rollLockInfo = new Dictionary<string, DateTime>();

        public static int GetRollLockTime(string fleetCode)
        {
            int lockSeconds = 900;
            if (_rollLockInfo.ContainsKey(fleetCode))
            {
                DateTime unLockTime = _rollLockInfo[fleetCode].AddSeconds(lockSeconds);
                if (DateTime.Now >= unLockTime)
                {
                    return 0;
                }
                else
                {
                    return (unLockTime - DateTime.Now).Seconds;
                }
            }
            return 0;
        }

        public static void AddRollLockInfo(string fleetCode)
        {
            if (_rollLockInfo.ContainsKey(fleetCode))
            {
                _rollLockInfo.Remove(fleetCode);
            }
            _rollLockInfo.Add(fleetCode, DateTime.Now);
        }

        public static void RemoveRollLockInfo(string fleetCode)
        {
            if (_rollLockInfo.ContainsKey(fleetCode))
            {
                _rollLockInfo.Remove(fleetCode);
            }
        }
    }
}
