using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO
{
    public class VisitReportUpDateModel
    {
        public VisitReportUpDateModel()
        {
            RqIds = new List<int>();
        }

        public int ReportId { get; set; }

        public string MonthlyAccount { get; set; }

        public decimal ProAmount { get; set; }

        public int ProductId { get; set; }

        public int ProfessionId { get; set; }

        public int VisitKind { get; set; }

        public int PhraseId { get; set; }

        public ICollection<int> RqIds { get; set; }

        public string VisitPersonName { get; set; }

        public string Remark { get; set; }

    }
}
