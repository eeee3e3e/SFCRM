using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class UserInfoRepository
    {
        public DTO.UserInfo GetUser(string loginName, string password)
        {
            var sqlStr = "select F_ID,F_Username,F_Password ,F_TypeID,F_DistrictCode from T_UserInfo where F_Username=@loginName and F_Password=@password";
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
            return new DTO.UserInfo() { LoginName = rows[0].ItemArray[1].ToString(), UserId = Convert.ToInt32(rows[0].ItemArray[0].ToString()), UserType = (DTO.Enum.UserType)Convert.ToInt32(rows[0].ItemArray[3].ToString()) };
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

       
    }
}
