using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DTO
{
    public class VisitReportModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string MonthlyAccount { get; set; }

        public decimal ProAccount { get; set; }

        public int ProductID { get; set; }

        public int ProfesionID { get; set; }

        public int KindID { get; set; }

        public int PhraseID { get; set; }

        public int StaffID { get; set; }

        public DateTime VisitDate { get; set; }

        public int RqID { get; set; }

        public string Remark { get; set; }

    }
}
