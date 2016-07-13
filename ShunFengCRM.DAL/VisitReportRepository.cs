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
            var sqlStr = @"select F_ID,CASE WHEN t4.VisitCount is null THEN 0 else t4.VisitCount end as VisitCount,Sorting from(
select t2.F_ID,t3.VisitCount,row_number()over(ORDER BY VisitCount desc) as Sorting from(
select * from T_UserInfo where F_TypeID=@typeID) as t2 left join(
select F_StaffID,Count(*) as VisitCount  from (

select F_ID,F_StaffID from dbo.T_VisitReport where F_VisitDate>=@visitDate and F_VisitDate<@endDate
) as t1  group by  F_StaffID) as t3 on t2.F_ID=t3.F_StaffID) as t4 where t4.F_ID=@userId";

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

        //public bool AddVisitRepor(VisitReportModel input)
        //{
        //    var sqlStr = "insert into  [SF_CRM].[dbo].[T_VisitReport] values(@Id,@ClientName,@MonthlyAccount,@ProAccount,@ProductID,@ProfessionID,@KindID,@PhaseID,@StaffID,@VisitDate)";
        //    SqlParameter[] parms = 
        //    {
        //        new SqlParameter("@Id",input.Id),
        //        new SqlParameter("@ClientName",input.ClientName),
        //        new SqlParameter("@MonthlyAccount",input.MonthlyAccount),
        //        new SqlParameter("@ProAccount",input.ProAccount),
        //        new SqlParameter("@ProductID",input.ProductID),
        //        new SqlParameter("@ProfessionID",input.ProfesionID),
        //        new SqlParameter("@KindID",input.KindID),
        //        new SqlParameter("@PhaseID",input.PhraseID),
        //        new SqlParameter("@StaffID",input.StaffID),
        //        new SqlParameter("@VisitDate",input.VisitDate),
        //    };

        //    var resultCount = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms, System.Data.CommandType.Text);
        //    if (resultCount > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

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

        public int UserCount(UserType userType)
        {
            var sqlStr = @"select count(*) from T_UserInfo where F_TypeID=@typeID";
            SqlParameter[] parms = 
            {
                new SqlParameter("@typeID",userType),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }

        public Dictionary<string, int> UserStandard()
        {
            var sqlStr = "SELECT * FROM [SF_CRM].[dbo].[T_VisitStandard]";
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return new Dictionary<string, int>();
            }
            var dic = new Dictionary<string, int>();
            dic.Add("visitStandard", Convert.ToInt32(rows[0].ItemArray[1].ToString()));
            dic.Add("warmRank", Convert.ToInt32(rows[0].ItemArray[2].ToString()));
            return dic;
        }

        public DataTable ShowReportListByCond(int userId, string keys)
        {
            string sqlStr = @"  select vr.F_ClientName, tr.F_CustomerName,vr.F_VisitDate,vr.F_ID 
                                from T_VisitReport vr left join  T_Remark tr on vr.F_ID=tr.F_VisitReportID
                                where vr.F_StaffID=@strUserID and F_ClientName like '%" + keys + @"%'
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

        public DataTable ShowReportList(int userId)
        {
            string sqlStr = @"  select vr.F_ClientName, tr.F_CustomerName,vr.F_VisitDate,vr.F_ID 
                                from T_VisitReport vr left join  T_Remark tr on vr.F_ID=tr.F_VisitReportID
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

        public bool UpdateVisitReport(VisitReportUpDateModel input)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("delete  [SF_CRM].[dbo].[T_RqInfo] where F_VisitReportID={0} ", input.ReportId));
            var rqIds = input.RqIdsStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in rqIds)
            {
                sb.Append(string.Format("insert into [SF_CRM].[dbo].[T_RqInfo] values({0},{1}) ", item, input.ReportId));
            }
            var sqlStr = sb.ToString();
            SqlParameter[] parms = 
            {
                
                //--@Id int,
                //--@MonthlyAccount varchar(50),
                //--@ProAmount decimal(18,0),
                //--@ProductId int,
                //--@ProfessionId int,
                //--@VisitKind int,
                //--@PhraseId int,
                //--@Sql varchar(max),
                //--@CustomerName nvarchar(max),
                //--@Remark nvarchar(max)

                new SqlParameter("@Id",input.ReportId),
                new SqlParameter("@MonthlyAccount",input.MonthlyAccount),
                new SqlParameter("@ProAmount",input.ProAmount),
                new SqlParameter("@ProductId",input.ProductId),
                new SqlParameter("@ProfessionId",input.ProfessionId),
                new SqlParameter("@VisitKind",input.VisitKind),
                new SqlParameter("@PhraseId",input.PhraseId),
                new SqlParameter("@Sql",sqlStr),
                new SqlParameter("@CustomerName",input.VisitPersonName),
                new SqlParameter("@Remark",input.Remark),

            };
            var result = new Tools.SqlHelper().ExecuteNonQuery("UpdateDate", parms, System.Data.CommandType.StoredProcedure);
            if (result > 0)
                return false;
            else
                return true;
        }

        public VisitReportModel GetVisitReport(int id, int userId)
        {
            var sqlReport = @"SELECT [F_ID]
                                  ,[F_ClientName]
                                  ,[F_MonthlyAccount]
                                  ,[F_ProAmount]
                                  ,[F_ProductID]
                                  ,[F_ProfessionID]
                                  ,[F_VisitKindID]
                                  ,[F_PhraseID]
                            FROM [SF_CRM].[dbo].[T_VisitReport] where F_ID=@Id  and F_StaffID=@UserId";
            SqlParameter[] parms =
            {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@Id",id),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlReport, parms, System.Data.CommandType.Text);
            if (result.Rows.Count != 1)
            {
                return null;
            }
            var row = result.Rows[0];
            var returnData = new VisitReportModel()
            {
                MonthlyAccount = row.ItemArray[2].ToString(),
                CustomerName = row.ItemArray[1].ToString(),
                PhraseId = Convert.ToInt32(row.ItemArray[7].ToString()),
                ProAmount = Convert.ToInt32(row.ItemArray[3].ToString()),
                ProductId = Convert.ToInt32(row.ItemArray[4].ToString()),
                ProfessionId = Convert.ToInt32(row.ItemArray[5].ToString()),
                ReportId = Convert.ToInt32(row.ItemArray[0].ToString()),
                VisitKind = Convert.ToInt32(row.ItemArray[7].ToString()),
            };
            var sqlRq = @"SELECT F_RqID FROM [SF_CRM].[dbo].[T_RqInfo] where F_VisitReportID=@ReportId";
            SqlParameter[] parmsRq =
            {
                new SqlParameter("@ReportId",id),
            };
            var resultRq = new Tools.SqlHelper().ExecuteQuery(sqlRq, parmsRq, System.Data.CommandType.Text);
            var rowsRq = resultRq.Rows;
            foreach (DataRow item in rowsRq)
            {
                returnData.RqIds.Add(Convert.ToInt32(item.ItemArray[0].ToString()));
            }
            var sqlRemark = @"  SELECT [F_Remark]
                                      ,[F_RemarkDate]
                                      ,[F_CustomerName]
                                  FROM [SF_CRM].[dbo].[T_Remark] where F_VisitReportID=@visitId";
            SqlParameter[] parmsRemark =
            {
                new SqlParameter("@visitId",id),
            };
            var resultRemark = new Tools.SqlHelper().ExecuteQuery(sqlRemark, parmsRemark, System.Data.CommandType.Text);
            var rowsRemark = resultRemark.Rows;
            foreach (DataRow item in rowsRemark)
            {
                var model = new RemarkInfo()
                {
                    Remark = string.IsNullOrEmpty(item.ItemArray[0].ToString()) ? "无" : item.ItemArray[0].ToString(),
                    Time = Convert.ToDateTime(item.ItemArray[1].ToString()),
                    VisitPersonName = string.IsNullOrEmpty(item.ItemArray[2].ToString()) ? "无" : item.ItemArray[2].ToString(),
                };
                returnData.RemarkInfos.Add(model);
            }
            return returnData;
        }

        public Nullable<DateTime> GetVisitReportTime(int id)
        {
            var sqlStr = "SELECT [F_VisitDate] FROM [SF_CRM].[dbo].[T_VisitReport]  where F_ID=@Id ";
            SqlParameter[] parms =
            {
                new SqlParameter("@Id",id),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            if (result.Rows.Count != 1)
            {
                return null;
            }
            return Convert.ToDateTime(result.Rows[0].ItemArray[0].ToString());
        }
    }
}
