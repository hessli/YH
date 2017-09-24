using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ConvertExtend
    {

        #region 枚举
        public static int ToInt(this System.Enum value)
        {
            var result = (int)System.Enum.Parse(value.GetType(), value.ToString());
            return result;
        }
        public static short ToShort(this System.Enum value)
        {
            return (short)value.ToInt();
        }

        #endregion

        #region 时间区域
        static DateTime StartTime = TimeZone.
            CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

       static  Dictionary<DayOfWeek, string> DicCurrentZoneWeek  = new Dictionary<DayOfWeek, string>();

        public static string ToWeek(DateTime time)
        {
            if (DicCurrentZoneWeek.Count == 0)
            {
                DicCurrentZoneWeek.Add(DayOfWeek.Monday, "周一");
                DicCurrentZoneWeek.Add(DayOfWeek.Tuesday, "周二");
                DicCurrentZoneWeek.Add(DayOfWeek.Wednesday, "周三");
                DicCurrentZoneWeek.Add(DayOfWeek.Thursday, "周四");
                DicCurrentZoneWeek.Add(DayOfWeek.Friday, "周五");
                DicCurrentZoneWeek.Add(DayOfWeek.Saturday, "周六");
                DicCurrentZoneWeek.Add(DayOfWeek.Sunday, "周日");
            }
            string week = string.Empty;
            if (time != default(System.DateTime))
            {
                week = DicCurrentZoneWeek[time.DayOfWeek];
            }
            return week;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static uint DateTimeToUInt(this DateTime time)
        {
            return (uint)(time - StartTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTimeToInt(this DateTime time)
        {
            return (int)(time - StartTime).TotalSeconds;
        }
        public static int DateTimeToInt(this DateTime? time)
        {
            if (time == null) return 0;
            DateTime t = (DateTime)time;
            return (int)(t - StartTime).TotalSeconds;
        }

        public static long DateTimeToIntMs(this DateTime time)
        {
            return (long)(time - StartTime).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeToLong(this DateTime time)
        {
            return (long)(time - StartTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式根据ms毫秒数转换为ms时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeMsToLong(this DateTime time)
        {
            return (long)(time - StartTime).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime时间格式根据ms毫秒数转换为ms时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTimeMsToLoginStr(this DateTime time)
        {
            return (time - StartTime).TotalMilliseconds.ToString();
        }

        /// <summary>
        /// Unix时间戳格式转换为DateTime时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime UIntToDateTime(this uint value)
        {
            return StartTime.AddSeconds(value);
        }

        /// <summary>
        /// Int时间戳格式转换为DateTime时间格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime IntToDateTime(this int value)
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
        public static int TimestampToAge(this int birthTimestamp)
        {
            var birthDate = IntToDateTime(birthTimestamp);
            return DateTimeToAge(birthDate);
        }

        /// <summary>
        /// 时间戳转年龄
        /// </summary>
        /// <param name="birthTimestamp">时间戳</param>
        /// <returns></returns>
        public static int TimestampToAge(this long birthTimestamp)
        {
            var birthDate = LongToDateTime(birthTimestamp);
            return DateTimeToAge(birthDate);
        }


        /// <summary>
        /// 日期转年龄
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static int DateTimeToAge(this DateTime birthDate)
        {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;
            return age;
        }

        #endregion
    }
}
