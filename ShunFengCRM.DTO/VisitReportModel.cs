using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO
{
    public class VisitReportModel : VisitReportUpDateModel
    {
        public VisitReportModel()
        {
            RemarkInfos = new List<RemarkInfo>();
        }

        public string CustomerName { get; set; }

        public ICollection<RemarkInfo> RemarkInfos { get; set; }
    }
}
