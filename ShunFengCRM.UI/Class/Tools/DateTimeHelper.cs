using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Class.Tools
{
    public class DateTimeHelper
    {
        public static DateTime GetThisWeekMonday(DateTime time)
        {
            var returnData = time;
            var week = time.DayOfWeek;
            switch (week)
            {
                case DayOfWeek.Friday:
                    returnData = returnData.AddDays(-4);
                    break;
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Saturday:
                    returnData = returnData.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    returnData = returnData.AddDays(-6);
                    break;
                case DayOfWeek.Thursday:
                    returnData = returnData.AddDays(-3);
                    break;
                case DayOfWeek.Tuesday:
                    returnData = returnData.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    returnData = returnData.AddDays(-2);
                    break;
            }
            return returnData.Date;
        }
    }
}