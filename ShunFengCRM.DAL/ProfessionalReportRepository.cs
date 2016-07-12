using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    class ProfessionalReportRepository
    {
        public bool InsertVisitRecord(  string strClientName, 
                                        string strAmount,  
                                        string strProduct,
                                        string strProfession,
                                        string strType,
                                        string strPhrase,
                                        string strCustomerName,
                                        string strRqArray,
                                        string strRemark   )
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
