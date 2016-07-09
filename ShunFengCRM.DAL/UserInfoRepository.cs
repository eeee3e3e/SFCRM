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



    }
}
