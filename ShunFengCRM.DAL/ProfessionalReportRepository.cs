using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ShunFengCRM.DAL
{
    public class ProfessionalReportRepository
    {
        public bool InsertVisitRecord(string strClientName,
                                        string strMonthlyAccount,
                                        string strAmount,
                                        string strProduct,
                                        string strProfession,
                                        string strType,
                                        string strPhrase,
                                        string strCustomerName,
                                        string strRqArray,
                                        string strRemark,
                                        string strStaffID)
        {   var strDate = DateTime.Now.Year.ToString() +"-" +DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            //insert into visitReport 
            var sqlStr = @"Insert into T_VisitReport(

                            F_ClientName,
							F_MonthlyAccount,
							F_ProAmount,
							F_ProductID,
							F_ProfessionID,
							F_VisitKindID,
							F_PhraseID,
							F_StaffID,
							F_VisitDate) values(@strClientName,
                                                @strMonthlyAccount, 
                                                @strAmount,  
                                                @strProduct,
                                                @strProfession,
                                                @strType,
                                                @strPhrase,
                                                @strStaffID,
                                                @strVisitDate
                                                 )";
                SqlParameter[] parms1 =
               {
                            new SqlParameter("@strClientName",strClientName),
                            new SqlParameter("@strMonthlyAccount",strMonthlyAccount),
                            new SqlParameter("@strAmount",strAmount),
                            new SqlParameter("@strProduct",strProduct),
                            new SqlParameter("@strProfession",strProfession),
                            new SqlParameter("@strType",strType),
                            new SqlParameter("@strPhrase",strPhrase),
                            new SqlParameter("@strStaffID",strStaffID),
                            new SqlParameter("@strVisitDate",strDate),
                  };
                
                var result = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms1, System.Data.CommandType.Text);
                
                //Get the inserted record ID.
                sqlStr = "select MAX(F_ID) from T_VisitReport";
                var dtResult= new Tools.SqlHelper().ExecuteQuery(sqlStr,System.Data.CommandType.Text);
                int i_ID = 0;
                if (dtResult.Rows.Count>0)
                {
                    i_ID = Convert.ToInt32(dtResult.Rows[0][0]);
                }

            //insert remark info into remark table.
             sqlStr = @"insert into T_Remark(F_VisitReportID,F_Remark,F_RemarkDate,F_CustomerName) values(@strVisitReportID,@strF_Remark,@strRemarkDate,@strCustomerName)";
            SqlParameter[] parms2 =
           {
                            new SqlParameter("@strVisitReportID",i_ID),
                            new SqlParameter("@strF_Remark",strRemark),
                            new SqlParameter("@strRemarkDate",strDate),
                            new SqlParameter("@strCustomerName",strCustomerName),
            };

            result = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms2, System.Data.CommandType.Text);


            //insert lsRq list table and build the relationship between visitReport and RqID
            string[] lsRqArray = strRqArray.Split(';');
                for (int iCount = 0; iCount < lsRqArray.Length; iCount++)
                {
                    string[] lsRqItems = lsRqArray[iCount].Split('*');
                    sqlStr = "insert into T_RqInfo (F_RqID,F_VisitReportID) values (@strRqID,@strVisitReportID)";
                    SqlParameter[] parms3 =
                    {   new SqlParameter("@strRqID", Convert.ToInt32(lsRqItems[0])),
                        new SqlParameter("@strVisitReportID", i_ID),
                    };
                    result = new Tools.SqlHelper().ExecuteNonQuery(sqlStr, parms3, System.Data.CommandType.Text);
                }


            return true;
        }
    }
}
