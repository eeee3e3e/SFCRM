using ShunFengCRM.UI.Class;
using ShunFengCRM.UI.Class.Tools;
using ShunFengCRM.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShunFengCRM.UI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录 赵凯 2016-07-07
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ActionResult Login(string loginName, string password)
        {
            var userId = new ShunFengCRM.DAL.UserInfoRepository().GetUser(loginName, password);
            ReturnData<string> data = null;
            if (userId != null)
            {
                data = new ReturnData<string>()
               {
                   //Data = "/home/SuccessLogin",
                   Data = "/home/Advertisement",
                   ErrorMessage = "成功",
                   ReturnType = ReturnType.Success,
                   WarnMessage = "成功",
               };
                //增加cookie验证
                Class.Tools.CookieHelper.SetCookie("userId", userId.ToString());
            }
            else
            {
                data = new ReturnData<string>()
               {
                   Data = "",
                   ErrorMessage = "登录名密码错误",
                   ReturnType = ReturnType.Fail,
                   WarnMessage = "登录名密码错误",
               };
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AuthenticationAttribute]
        public ActionResult SuccessLogin()
        {
            return View();
        }
        [AuthenticationAttribute]
        public ActionResult Advertisement()
        {
            var imgStr = new ShunFengCRM.DAL.AdvertyRepository().GetAdverty();
            ViewBag.GuangGao = imgStr;
            return View();
        }
        [AuthenticationAttribute]
        public ActionResult Mainfrm()
        {
            return View();
        }
        public ActionResult MainfrmAjax()
        {
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().GetUserInfo(base.UserId);
            var typeName = new ShunFengCRM.DAL.UserTypeRepository().GetUserTypeName(userInfo.UserType);
            var typeCount = new ShunFengCRM.DAL.UserInfoRepository().GetUserTypeCount(userInfo.UserType);
            var sorting = new ShunFengCRM.DAL.VisitReportRepository().GetUserRank(UserId, userInfo.UserType, Class.Tools.DateTimeHelper.GetThisWeekMonday(DateTime.Now), DateTime.Now);
            var userData = new ReturnUserInfo()
            {
                CurrentWeekSort = sorting,
                LoginName = userInfo.LoginName,
                UserID = userInfo.UserId,
                UserName = userInfo.UserName,
            };
            var anonymousData = new
            {
                UserInfo = userData,
                TypeNameSumStr = string.Format("{0}位{1}", typeCount, typeName),
            };
            var data = new ReturnData<Object>()
            {
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
                Data = anonymousData,
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AuthenticationAttribute]
        public ActionResult PersonalEdit()
        {
            return View();
        }
        [AuthenticationAttribute]
        public ActionResult PersonalEditAjax(string strPassword)
        {
            var strID = Class.Tools.CookieHelper.GetCookie("userId");

            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().GetUser(strID);
            ReturnData<string> data = null;


            if (userInfo)
            {
                //edit successful
                data = new ReturnData<string>
                {
                    Data = "/home/Mainfrm",
                    ErrorMessage = "成功",
                    ReturnType = ReturnType.Success,
                    WarnMessage = "成功",
                };

            }
            else
            {
                //edit fail
                data = new ReturnData<string>
                {
                    Data = "/home/PersonalEdit",
                    ErrorMessage = "失败",
                    ReturnType = ReturnType.Fail,
                    WarnMessage = "失败",
                };

            }

            // return json data
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //bind personal information
        public  ActionResult BindingPersonalInfo()
        {
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().GetUserBasicInfo(strID);
            ReturnData<string> data = null;


            if (userInfo!=null)
            {
                //edit successful
                data = new ReturnData<string>
                {
                    Data = JsonConverter.ObjectToJson(userInfo),
                    ErrorMessage = "成功",
                    ReturnType = ReturnType.Success,
                    WarnMessage = "成功",
                };

            }
            else
            {
                //edit fail
                data = new ReturnData<string>
                {
                    Data = null,
                    ErrorMessage = "失败",
                    ReturnType = ReturnType.Fail,
                    WarnMessage = "失败",
                };

            }

            // return json data
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult StatusReminder()
        {
            return View();
        }

        public ActionResult Visit_Record_Add()
        {
            return View();    
        }

        public ActionResult Visit_Record_Add(  string strClientName,string strMonthlyAccountNo,
                                               string strAmount,
                                               string strProduct,
                                               string strProfession,
                                               string strType,
                                               string strPhrase,
                                               string strCustomerName,
                                               string strRqArray,
                                               string strRemark)
        {
            
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().InsertVisitRecord(strClientName, strAmount, strProduct, strProfession, strType, strPhrase, strCustomerName, strRqArray, strRemark);
                                                                                      
            ReturnData<string> data = null;                                           
            data = new ReturnData<string>                                             
            {                                                                         
                Data = null,                                                          
                ErrorMessage = "失败",                                                
                ReturnType = ReturnType.Fail,                                         
                WarnMessage = "失败",                                                 
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Visit_Record_List()
        {
            return View();
        }




        public ActionResult VistRecordListSelectAjax(string strCond)
        {
            string strReturn = "";
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            DataTable ReportList = new ShunFengCRM.DAL.VisitReportRepository().ShowReportListByCond(strID, strCond);
            ReturnData<string> data = null;

            strReturn += @"<table style='width: 100%;'>";

            if (ReportList != null)
            {
                //edit successful
                for (int iCount = 0; iCount < ReportList.Rows.Count; iCount++)
                {
                    strReturn += @"<tr style='width: 100 %; onclick='SysLogin('Visit_Record_Edit')' '> ";
                    strReturn += @"<td style = 'text-align: center;'> ";
                    strReturn += @"<div>";
                    strReturn += @"<div class='vrl_items' style='font-size: 20px; font-weight: bold'>" + ReportList.Rows[iCount]["F_ClientName"].ToString() + "</div>";
                    strReturn += @"<div style = 'text-align: center'>";
                    strReturn += @"拜访人:" + ReportList.Rows[iCount]["F_CustomerName"].ToString();
                    strReturn += @"</div>";
                    strReturn += @"</div>";
                    strReturn += @"</td>";
                    strReturn += @" ";
                    strReturn += @"</tr>";
                    strReturn += @"<tr>";
                    strReturn += @"<td>";
                    strReturn += @"<div style = 'border-bottom: 1px dotted  #245580 ; border-radius: 5px;text-align: right'>";
                    strReturn += @"<div class='fr vrl_items'>";
                    strReturn += ReportList.Rows[iCount]["F_VisitDate"].ToString();
                    strReturn += @"</div>";
                    strReturn += @"<div id ='' class='fr vrl_items' style=''>录入时间：</div>";
                    strReturn += @"</div>";
                    strReturn += @"</td>";
                    strReturn += @"</tr>";

                }


            }
            else
            {
                //edit fail
                strReturn += @"<tr style='width: 100 %;'> ";
                strReturn += @"<td style = 'text-align: center;'> ";
                strReturn += "暂时没有相关记录";
                strReturn += @"</td>";
                strReturn += @"</tr>";

            }

            strReturn += "</table >";


            data = new ReturnData<string>
            {
                Data = strReturn,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            // return json data
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult VistRecordListAjax()
        {
            string strReturn = "";
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            DataTable ReportList = new ShunFengCRM.DAL.VisitReportRepository().ShowReportList(strID);
            ReturnData<string> data = null;

            strReturn += @"<table style='width: 100%;'>";

            if (ReportList != null)
            {
                //edit successful
                for (int iCount = 0; iCount < ReportList.Rows.Count; iCount++)
                {
                    strReturn += @"<tr style='width: 100 %; onclick='SysLogin('Visit_Record_Edit')' '> ";
                     strReturn +=@"<td style = 'text-align: center;'> ";
                    strReturn += @"<div>";
                     strReturn +=@"<div class='vrl_items' style='font-size: 20px; font-weight: bold'>"+ ReportList.Rows[iCount]["F_ClientName"].ToString() + "</div>";
                     strReturn +=@"<div style = 'text-align: center'>";
                     strReturn +=@"拜访人:" + ReportList.Rows[iCount]["F_CustomerName"].ToString();
                     strReturn +=@"</div>";
                     strReturn +=@"</div>";
                     strReturn +=@"</td>";
                     strReturn +=@" ";
                     strReturn +=@"</tr>";
                     strReturn +=@"<tr>";
                     strReturn +=@"<td>";
                     strReturn +=@"<div style = 'border-bottom: 1px dotted  #245580 ; border-radius: 5px;text-align: right'>";
                     strReturn +=@"<div class='fr vrl_items'>";
                     strReturn += ReportList.Rows[iCount]["F_VisitDate"].ToString();
                     strReturn +=@"</div>";
                     strReturn +=@"<div id ='' class='fr vrl_items' style=''>录入时间：</div>";
                     strReturn +=@"</div>"                                                                                            ;
                     strReturn +=@"</td>";
                     strReturn +=@"</tr>";            

                }
                

            }
            else
            {
                //edit fail
                strReturn += @"<tr style='width: 100 %;'> ";
                strReturn += @"<td style = 'text-align: center;'> ";
                strReturn += "暂时没有相关记录";
                strReturn += @"</td>";
                strReturn += @"</tr>";

            }

            strReturn +="</table >";


            data = new ReturnData<string>
            {
                Data = strReturn,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            // return json data
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Visit_Record_Edit()
        {
            return View();
        }

        public ActionResult StatusReminderAjax()
        {
            var visitReportRepository = new DAL.VisitReportRepository();
            var startTime = Class.Tools.DateTimeHelper.GetThisMonthFist(DateTime.Now);
            var endTime = Class.Tools.DateTimeHelper.GetThisMonthFist(DateTime.Now.AddMonths(1));
            var userReportInfo = new UserReportInfo()
            {
                OneMonthNewSign = visitReportRepository.OneMonthNewSign(base.UserId, startTime, endTime),
                OneMonthVisiCount = visitReportRepository.OneMonthVisitCount(base.UserId, startTime, endTime),
                OneMonthVisitSort = visitReportRepository.GetUserRank(base.UserId, base.UserType, startTime, endTime),
                TodayVisitCount = visitReportRepository.TodayVisitCustomer(base.UserId),
                VisitReportRqCount = visitReportRepository.VisitReportRqCount(base.UserId),
                VisitCount = visitReportRepository.VisitCount(base.UserId),
                IsRed = false,
            };
            var dic = visitReportRepository.UserStandard();
            var userCount = visitReportRepository.UserCount(base.UserType);
            if (userReportInfo.OneMonthVisiCount < dic["visitStandard"])
            {
                if (userReportInfo.OneMonthVisitSort + dic["warmRank"] >= userCount)
                    userReportInfo.IsRed = true;
            }
            var data = new ReturnData<UserReportInfo>()
            {
                Data = userReportInfo,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
