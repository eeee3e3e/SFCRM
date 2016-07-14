using ShunFengCRM.DTO;
using ShunFengCRM.DTO.Enum;
using ShunFengCRM.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShunFengCRM.DAL
{
    public class UserInfoRepository
    {
        public Nullable<int> GetUser(string loginName, string password)
        {
            var sqlStr = "select F_ID from T_UserInfo where F_Username=@loginName and F_Password=@password";
            SqlParameter[] parms = 
            {
                new SqlParameter("@loginName",loginName),
                new SqlParameter("@password",password),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return null;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }


        public bool GetUser(int userId)
        {
            var sqlStr = "select F_ID,F_Username,F_Password ,F_TypeID,F_DistrictCode from T_UserInfo where F_ID=@userId";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userId",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return false;
            }
            return true;
        }

        public UserInfo GetUserInfo(int userId)
        {
            var sqlStr = @"select t1.F_ID,t1.F_Name,t2.F_Username,t2.F_TypeID from (
            SELECT *
              FROM T_UserDetail where F_ID=@userId) as t1 inner join ( 
            SELECT *
              FROM T_UserInfo where F_ID=@userId) as t2 on t1.F_ID=t2.F_ID";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userId",userId),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return null;
            }
            return new UserInfo()
            {
                LoginName = rows[0].ItemArray[2].ToString(),
                UserId = Convert.ToInt32(rows[0].ItemArray[0].ToString()),
                UserName = rows[0].ItemArray[1].ToString(),
                UserType = (DTO.Enum.UserType)Convert.ToInt32(rows[0].ItemArray[3].ToString())
            };
        }

        #region get user basic info- add by YangDu
        public UserBasicInfo GetUserBasicInfo(int iUserID)
        {
            /*select * from T_UserInfo ui left join T_UserDetail ud on ui.F_ID=ud.F_ID 
left join T_Orgnization org on ud.F_OrgCode=org.F_ID where ui.F_ID=1*/
            var sqlStr = @" select  ud.F_Name,ud.F_StaffNo,ud.F_OrgCode,org.F_OrgName from T_UserInfo ui 
                            left join T_UserDetail ud on ui.F_ID=ud.F_ID 
                            left join T_Orgnization org on ud.F_OrgCode=org.F_ID 
                            where ui.F_ID=@userId";
            SqlParameter[] parms =
            {
                new SqlParameter("@userId",iUserID),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return null;
            }
            return new UserBasicInfo()
            {
                NickName = rows[0].ItemArray[0].ToString(),
                StaffNo = rows[0].ItemArray[1].ToString(),
                OrgCode = rows[0].ItemArray[2].ToString(),
                OrgName = rows[0].ItemArray[3].ToString()
            };

        }

        #endregion



        public int GetUserTypeCount(UserType userType)
        {
            var sqlStr = "SELECT Count(*) as SumCount FROM [SF_CRM].[dbo].[T_UserInfo] where F_TypeID=@userType";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userType",(int)userType),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return 0;
            }
            return Convert.ToInt32(rows[0].ItemArray[0].ToString());
        }

        public bool EditUser(int strID, string strPassword)
        {
            var sqlStr = "update T_UserInfo set f_password=@strPassword where f_id=@strUserID";
            SqlParameter[] parms =
            {
                new SqlParameter("@strUserID",strID),
                new SqlParameter("@strPassword",strPassword),
            };
            var result = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms, System.Data.CommandType.Text);

            if (result >= 1)
            {
                return true;
            }
            else
                return false;
        }

        public DataTable GetTop10byMonth(string strPosition)
        {
            //1.compare the staff level
            //ranklist includes : 1. report number, 2. ranking no, 3, district-name 4, staff name
            string strDateStart = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "01";
            string strDateEnd = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-"+ DateTime.Now.Day.ToString();
            string sqlStr= @"select * from 
                            (
                                select  distinct row_number() over(order by (select 0)) as RankID, ud.F_Name, ut.F_Name as strLevel,ui.F_ID as F_ID, org.F_OrgName as F_OrgName, sum (case when vr.F_ID is null then 0 else 1 end)as TotalVisit 
                                from T_UserInfo ui left join (select * from T_VisitReport where F_VisitDate between @strDateStart and @strDateEnd) vr
                                on vr.F_StaffID = ui.F_ID left join T_UserType ut 
                                on ui.F_TypeID=ut.F_TypeID 
                                left join T_UserDetail ud on ui.F_ID=ud.F_ID
                                left join T_Orgnization org on ud.F_OrgCode=org.F_ID where ut.F_Name=@strPosition 
                                group by ui.F_ID,ut.F_Name,ui.f_id, ud.F_Name,org.F_OrgName
                             )       
                                tb order by TotalVisit desc";

            SqlParameter[] parms =
            {
                new SqlParameter("@strDateStart",strDateStart),
                new SqlParameter("@strDateEnd",strDateEnd),
                new SqlParameter("@strPosition",strPosition),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);

            if (result.Rows.Count != 0)
            {

                return result;
            }
            else
                return null;

        }

        

        //get rank name according to user id .add by YangDu
        public string GetRankName(string strID)
        {
            var sqlStr = @"select ut.F_Name from t_userinfo ui inner join T_UserType ut 
                        on ui.F_TypeID=ut.F_TypeID where ui.F_ID = @strUserID";
            SqlParameter[] parms =
            {
                new SqlParameter("@strUserID",strID),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);

            if (result.Rows.Count!=0)
            {
                return result.Rows[0].ItemArray[0].ToString();
            }
            else
                return null;
        }

    }
}
