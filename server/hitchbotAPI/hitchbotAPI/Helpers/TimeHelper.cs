using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Helpers
{
    public class TimeHelper
    {
        public static string GetHourFromUnixTime(double unixTimeStamp)
        {
            return string.Format("{0: %H:mmtt}", UnixTimeStampToDateTime(unixTimeStamp));
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}