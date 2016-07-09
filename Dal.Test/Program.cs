using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //var userInfo = new List<int>() { 1, 6, 7, 9, 10, 11, 12, 13, 14, 15 };

            //var xx = new ShunFengCRM.DAL.UserInfoRepository().GetUser("sun", "123");
            //for (int i = 0; i < 100; i++)
            //{
            //    Random r = new Random();
            //    var x = r.Next(1, 10);
            //    var item = new ShunFengCRM.DTO.VisitReportModel();
            //    item.ClientName = i.ToString();
            //    item.Id = i + 4;
            //    item.KindID = 1;
            //    item.MonthlyAccount = "10000";
            //    item.PhraseID = 1;
            //    item.ProAccount = 1;
            //    item.ProductID = 1;
            //    item.ProfesionID = 1;
            //    item.Remark = "";
            //    item.RqID = 1;
            //    item.StaffID = userInfo[x];
            //    item.VisitDate = DateTime.Now.AddHours(-i);
            //    var model = new ShunFengCRM.DAL.VisitReportRepository().AddVisitRepor(item);
            //}
            #endregion

            var dt = DateTime.Today.DayOfWeek;
        }
    }
}
