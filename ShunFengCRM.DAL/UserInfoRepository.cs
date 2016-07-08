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

        public string GetAdverty()
        {
            var sqlStr = "SELECT TOP 1 [F_AdUrl] FROM [SF_CRM].[dbo].[T_Advertise]";
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return null;
            }
            return rows[0].ItemArray[0].ToString();
        }


        public IList<string> fwr()
        {
            var results = new List<string>();
            var sqlStr = "SELECT TOP 2 [F_AdUrl] FROM [SF_CRM].[dbo].[T_Advertise]";
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, System.Data.CommandType.Text);
            var rows = result.Rows;
            foreach (DataRow item in rows)
            {
                results.Add(item.ItemArray[0].ToString());
            }
            return results;
        }


    }
}
