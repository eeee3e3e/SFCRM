using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Models
{
    public class UserReportInfo
    {
        public int TodayVisitCount { get; set; }

        public int OneMonthVisiCount { get; set; }

        public int OneMonthVisitSort { get; set; }

        public int OneMonthNewSign { get; set; }

        public int VisitReportRqCount { get; set; }

        public int VisitCount { get; set; }
    }
}