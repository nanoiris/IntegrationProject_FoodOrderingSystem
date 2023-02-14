using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class DateUtils
    {
        public static int GetWeekIndexByYear(DateTime dt)
        {
            DateTime time = Convert.ToDateTime(dt.ToString("yyyy") + "-01-01");
            TimeSpan ts = dt - time;
            
            int firstDayOfWeek = (int)time.DayOfWeek;
            int day = int.Parse(ts.TotalDays.ToString("F0")) + 1;
            if (firstDayOfWeek == 0)
            {
                day--;
            }
            else
            {
                day = day - (7 - firstDayOfWeek + 1);
            }
            int thisDayOfWeek = (int)dt.DayOfWeek;
            if (thisDayOfWeek == 0)
            {
                day = day - 7;
            }
            else
            {
                day = day - thisDayOfWeek;
            }
            int week = (day + 7 + 7) / 7;
            return week;
        }
    }
}
