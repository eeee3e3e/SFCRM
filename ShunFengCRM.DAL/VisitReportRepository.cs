using ShunFengCRM.DTO;
using ShunFengCRM.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class VisitReportRepository
    {
        public int GetUserRank(int userId, UserType userType, DateTime visitDate, DateTime endDate)
        {
            var sqlStr = @"select F_StaffID,VisitCount,Sorting from(
select t3.F_StaffID,t3.VisitCount,row_number()over(ORDER BY VisitCount desc) as Sorting from(
select * from T_UserInfo where F_TypeID=@typeID) as t2 inner join(
select F_StaffID,Count(*) as VisitCount  from (

select F_ID,F_StaffID from dbo.T_VisitReport where F_VisitDate>=@visitDate and F_VisitDate<@endDate
) as t1  group by  F_StaffID) as t3 on t2.F_ID=t3.F_StaffID) as t4 where t4.F_StaffID=@userId";

            SqlParameter[] parms = 
            {
                new SqlParameter("@typeID",userType),
                new SqlParameter("@visitDate",visitDate),
                new SqlParameter("@userId",userId),
                new SqlParameter("@endDate",endDate)
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
            var sqlStr = "insert into  [SF_CRM].[dbo].[T_VisitReport] values(@Id,@ClientName,@MonthlyAccount,@ProAccount,@ProductID,@ProfessionID,@KindID,@PhaseID,@StaffID,@VisitDate)";
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


        public int TodayVisitCustomer(int userId)
        {
            var sqlStr = "SELECT Count(*) as VisitCount FROM [SF_CRM].[dbo].[T_VisitReport] where F_VisitDate>=@today and F_StaffID=@userId";

            SqlParameter[] parms = 
            {
                new SqlParameter("@today",DateTime.Now.Date),
                new SqlParameter("@userId",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }

        public int OneMonthNewSign(int userId, DateTime startDate, DateTime endDate)
        {
            var sqlStr = "SELECT Count(*) FROM [SF_CRM].[dbo].[T_VisitReport] where F_PhraseID=3 and F_VisitKindID=1 and F_StaffID=@userId and F_VisitDate>=@startDate and F_VisitDate<@endDate";
            SqlParameter[] parms = 
            {
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@userId",userId),
                new SqlParameter("@endDate",endDate),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }

        public int OneMonthVisitCount(int userId, DateTime startDate, DateTime endDate)
        {
            var sqlStr = "SELECT Count(*) FROM [SF_CRM].[dbo].[T_VisitReport] where F_StaffID=@userId and F_VisitDate>=@startDate and F_VisitDate<@endDate";
            SqlParameter[] parms = 
            {
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@userId",userId),
                new SqlParameter("@endDate",endDate),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }
        public int VisitCount(int userId)
        {
            var sqlStr = "SELECT Count(*) FROM [SF_CRM].[dbo].[T_VisitReport] where F_StaffID=@userId";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userId",userId),

            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }

        public int VisitReportRqCount(int userId)
        {
            var sqlStr = "select Count(*) from (select * from  [SF_CRM].[dbo].[T_VisitReport] where F_StaffID=@userId) as t1 inner join(SELECT distinct [F_VisitReportID]FROM [SF_CRM].[dbo].[T_RqInfo]) as t2 on t1.F_ID=t2.F_VisitReportID";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userId",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }


        public DataTable ShowReportList(int userId)
        {
            string sqlStr = @"  select vr.F_ClientName, vr.F_CustomerName,vr.F_VisitDate,vr.F_ID 
                                from T_VisitReport vr 
                                where vr.F_StaffID=@strUserID
                                order by F_VisitDate desc";

            SqlParameter[] parms =
            {
                new SqlParameter("@strUserID",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);

            if (result.Rows.Count != 0)
            {

                return result;
            }
            else
                return null;
        }
    }
}
