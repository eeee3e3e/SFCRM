using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO
{
    public class RemarkInfo
    {
        public DateTime Time { get; set; }

        public string TimeStr { get { return Time.ToString("yyyy-MM-dd"); } }

        public string Remark { get; set; }

        public string VisitPersonName { get; set; }
    }
}
