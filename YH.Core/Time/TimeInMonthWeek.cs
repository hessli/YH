using System;


namespace YH.Core.Time
{
  public  class TimeInMonthWeek
    {
        public TimeInMonthWeek(DateTime currentTime)
        {
            _currentTime = currentTime;

            this.CurrentDate =DateTime.Parse(_currentTime.ToShortDateString());

            SetWeek();

            YearMoth = _currentTime.GetDateTimeFormats('y')[0].ToString();
        }

        private DateTime _currentTime;

        private int _day;
        /// <summary>
        /// 本月第一天是周几
        /// </summary>
        private int _weekday;
        /// <summary>
        /// 本月第一周有几天
        /// </summary>

        private int _firstWeekEndDay;

        /// <summary>
        /// 本月第一天
        /// </summary>
        private DateTime _firstDay;
        /// <summary>
        /// 当前日期和第一周之差
        /// </summary>
        private int _diffday;

        private  void SetWeek()
        {
            _day = _currentTime.Day;

            //本月第一天

            _firstDay = _currentTime.AddDays(1 - _day);

            //本月第一天是周几

             _weekday = (int)_firstDay.DayOfWeek == 0 ? 7 : (int)_firstDay.DayOfWeek;

            //本月第一周有几天

             _firstWeekEndDay = 7 - (_weekday - 1);

            //当前日期和第一周之差

             _diffday = _day - _firstWeekEndDay;

            _diffday = _diffday > 0 ? _diffday : 1;

            //当前是第几周,如果整除7就减一天

            Week=((_diffday % 7) == 0

             ? (_diffday / 7 - 1)

             : (_diffday / 7)) + 1 + (_day > _firstWeekEndDay ? 1 : 0);
              
        }

         public int Week { get; private  set; }

         public DateTime CurrentDate { get; private set; }

         public string YearMoth { get; private set; }
    }
}
