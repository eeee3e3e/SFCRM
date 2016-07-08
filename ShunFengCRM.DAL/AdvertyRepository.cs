using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShunFengCRM.DAL
{
    public class AdvertyRepository
    {
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
    }
}
