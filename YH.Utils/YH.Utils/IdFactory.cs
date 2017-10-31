using System;
using System.Time;
using YH.Core.Math;

namespace YH.Utils
{
    public class IdFactory
    {
        public static string CreateNo()
        {
            long id = 0;
            RandHelper helper = new RandHelper();
            string strid = TimeHelper.DateTimeToLong(DateTime.Now) + helper.GenerateCheckCodeNum(4);
            if (Int64.TryParse(strid, out id))
            {
                return id.ToString();
            }
            return "0";
        }
    }
}
