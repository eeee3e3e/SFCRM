using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Models
{
    public class ReturnUserInfo
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string LoginName { get; set; }

        public int CurrentWeekSort { get; set; }
    }
}