using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.DateTimeHelper
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertToTImezone(DateTime date, string timeZone)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            if (timeZoneInfo != null)
            {
                date = TimeZoneInfo.ConvertTimeFromUtc(date, timeZoneInfo);
            }
            return date;
        }
    }
}
