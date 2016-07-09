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

        #region 修改用户密码
        /// <summary>
        /// modify user password
        /// </summary>
        /// <param name="strUsername">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <returns></returns>
        public bool EditUser(int strId, string strPassword)
        {
            var sqlStr = "update T_UserInfo set f_password ='@strPassword' where f_username = @strId";
            SqlParameter[] parms =
            {
                new SqlParameter("@strId",strId),
                new SqlParameter("@strPassword",strPassword)
            };

            var result=new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms, System.Data.CommandType.Text);

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}
