using ShunFengCRM.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class UserTypeRepository
    {
        public string GetUserTypeName(UserType userType)
        {
            var sqlStr = "SELECT Count(*) as SumCount FROM T_UserType where F_TypeID=@userType";
            SqlParameter[] parms = 
            {
                new SqlParameter("@userType",(int)userType),
            };
            var result = new Tools.SqlHelper().ExecuteQuery(sqlStr, parms, System.Data.CommandType.Text);
            var rows = result.Rows;
            if (rows.Count != 1)
            {
                return "";
            }
            return rows[0].ItemArray[0].ToString();
        }
    }
}
