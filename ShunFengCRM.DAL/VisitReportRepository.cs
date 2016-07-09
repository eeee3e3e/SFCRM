using ShunFengCRM.DTO;
using ShunFengCRM.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class VisitReportRepository
    {
        public int GetUserRank(int userId, UserType userType, DateTime visitDate)
        {
            var sqlStr = @"select F_StaffID,VisitCount,Sorting from(
select t3.F_StaffID,t3.VisitCount,row_number()over(ORDER BY VisitCount desc) as Sorting from(
select * from T_UserInfo where F_TypeID=@typeID) as t2 inner join(
select F_StaffID,Count(*) as VisitCount  from (

select F_ID,F_StaffID from dbo.T_VisitReport where F_VisitDate>=@visitDate
) as t1  group by  F_StaffID) as t3 on t2.F_ID=t3.F_StaffID) as t4 where t4.F_StaffID=@userId";

            SqlParameter[] parms = 
            {
                new SqlParameter("@typeID",userType),
                new SqlParameter("@visitDate",visitDate),
                new SqlParameter("@userId",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[2].ToString());
        }

        public bool AddVisitRepor(VisitReportModel input)
        {
            var sqlStr = "insert into  [SF_CRM].[dbo].[T_VisitReport] values(@Id,@ClientName,@MonthlyAccount,@ProAccount,@ProductID,@ProfessionID,@KindID,@PhaseID,@StaffID,@VisitDate,@RqID,@Remark)";
            SqlParameter[] parms = 
            {
                new SqlParameter("@Id",input.Id),
                new SqlParameter("@ClientName",input.ClientName),
                new SqlParameter("@MonthlyAccount",input.MonthlyAccount),
                new SqlParameter("@ProAccount",input.ProAccount),
                new SqlParameter("@ProductID",input.ProductID),
                new SqlParameter("@ProfessionID",input.ProfesionID),
                new SqlParameter("@KindID",input.KindID),
                new SqlParameter("@PhaseID",input.PhraseID),
                new SqlParameter("@StaffID",input.StaffID),
                new SqlParameter("@VisitDate",input.VisitDate),
                new SqlParameter("@RqID",input.RqID),
                new SqlParameter("@Remark",input.Remark),
            };

            var resultCount = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms, System.Data.CommandType.Text);
            if (resultCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
