using ShunFengCRM.DTO;
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
        [AuthenticationAttribute]
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

            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().EditUser(strID, strPassword);
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
        [AuthenticationAttribute]
        public ActionResult BindingPersonalInfo()
        {
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().GetUserBasicInfo(strID);
            ReturnData<string> data = null;


            if (userInfo != null)
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

        [AuthenticationAttribute]
        public ActionResult StatusReminder()
        {
            return View();
        }

        [AuthenticationAttribute]
        public ActionResult Visit_Record_Add()
        {
            return View();
        }

        [AuthenticationAttribute]
        public ActionResult VisitRecordAdd(string strClientName,
                                               string strMonthlyAccountNo,
                                               string strAmount,
                                               string strProduct,
                                               string strProfession,
                                               string strType,
                                               string strPhrase,
                                               string strCustomerName,
                                               string strRqArray,
                                               string strRemark)
        {

            var strStaffID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            var VisitRecordAdd = new ShunFengCRM.DAL.ProfessionalReportRepository();
            var result = VisitRecordAdd.InsertVisitRecord(strClientName, strMonthlyAccountNo, strAmount, strProduct, strProfession, strType, strPhrase, strCustomerName, strRqArray, strRemark, strStaffID.ToString());

            ReturnData<string> data = null;
            data = new ReturnData<string>
            {
                Data = null,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AuthenticationAttribute]
        public ActionResult Visit_Record_List()
        {
            return View();
        }



        [AuthenticationAttribute]
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
                    strReturn += @"<div onclick='turnPage(" + ReportList.Rows[iCount]["F_ID"] + ")'>";
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

        public ActionResult RankType()
        {
            return View();
        }

        public ActionResult RankDetail()
        {
            return View();
        }
        //Rank according to the user level
        [Authentication]
        public ActionResult RankByLevelAjax(string strPosition)
        {
            int iFlag = 0;
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            //return userinfo
            var RankInfo = new ShunFengCRM.DAL.UserInfoRepository().GetTop10byMonth(strPosition);
            ReturnData<string> data = null;
            string strHtml = "";

            string strMyVisitNum = "";
            string strMyRankNum = "";
            string strMyOrg = "";

            if (RankInfo != null)
            {
                if (RankInfo.Rows.Count > 0)
                {

                    for (int iCount = 0; iCount < RankInfo.Rows.Count; iCount++)
                    {
                        //Get the input rows
                        if (RankInfo.Rows[iCount]["F_ID"].ToString().Trim() == strID.ToString().Trim())
                        {
                            strMyVisitNum = RankInfo.Rows[iCount]["TotalVisit"].ToString();
                            strMyRankNum = RankInfo.Rows[iCount]["RankID"].ToString();
                            strMyOrg = "您的排名";
                        }

                        if (iCount == 0)
                        {
                            if (RankInfo.Rows[iCount]["strLevel"].ToString().Trim() == strPosition.Trim())
                            {
                                iFlag = 1;  //判断身份标准
                            }
                            strHtml += @"<div class='DefaultLine' style='height: 80px;'>
                                               <label class='vertical-center Font_Color_w' style=' text-align: center;font-size: 20px;height:80px; width: 40%;'> 
  				                                " + RankInfo.Rows[iCount]["TotalVisit"].ToString() + @"
  			                                </label>
			                                 <label class='vertical-center Font_Color_w' style='text-align: left;font-size: 20px;height:60px; width: 40%;'> 
  					                               " + RankInfo.Rows[iCount]["RankID"].ToString() + ". " + RankInfo.Rows[iCount]["F_OrgName"].ToString() + "-" + RankInfo.Rows[iCount]["F_Name"].ToString() + @"
                                              </label>
  		                                </div>	";


                        }
                        else if (iCount < 8)
                        {
                            strHtml += @"<div style='height: 30px; background-color:#FFFFFF'>
  		                                <label  style = 'text-align: center;font-size: 15px;color:#2575cf;  height:30px; width: 40%; ' >
                                              " + RankInfo.Rows[iCount]["TotalVisit"].ToString() + @"
                                           </label>
                                          <label style = 'text-align: left;font-size: 15px;color:#2575cf; height:30px; width: 40%;' >
                                               " + RankInfo.Rows[iCount]["RankID"].ToString() + ". " + RankInfo.Rows[iCount]["F_OrgName"].ToString() + "-" + RankInfo.Rows[iCount]["F_Name"].ToString() + @"
                                            </label >
                                        </div> ";


                        }
                        else
                        {
                            break;
                        }
                    }

                }
                else
                {
                    strHtml = "<Label style='font-color:#000000; text-align:center'>没有相关数据</Label>";
                }

                if (iFlag == 1)
                {
                    strHtml += @"<div style='height: 40px; margin-top: 10px; padding-top: 10px; background-color: #2575cf;'> 
  				                    <label class='Font_Color_w' style='height:30px; width: 40%; text-align: center;font-size: 18px'>" + strMyVisitNum + @"</label>
			 
  				                    <label class='Font_Color_w' style='height:30px; width: 40%; text-align: left;font-size: 18px'>
  					                    " + strMyRankNum + "." + strMyOrg + @"
                                      </label>
  			                    </div>";
                }
                //edit successful
                data = new ReturnData<string>
                {
                    Data = strHtml,
                    ErrorMessage = "成功",
                    ReturnType = ReturnType.Success,
                    WarnMessage = "成功",
                };

            }
            else
            {
                strHtml = "<Label style='font-color:#000000; text-align:center'>没有相关数据</Label>";
                //edit fail
                data = new ReturnData<string>
                {
                    Data = strHtml,
                    ErrorMessage = "失败",
                    ReturnType = ReturnType.Fail,
                    WarnMessage = "失败",
                };

            }

            // return json data
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [AuthenticationAttribute]
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
                    strReturn += @"<td style = 'text-align: center;'> ";
                    strReturn += @"<div onclick='turnPage(" + ReportList.Rows[iCount]["F_ID"] + ")'>";
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

        [AuthenticationAttribute]
        public ActionResult VisitRecordEdit(int id)
        {
            ViewBag.ReportId = id;
            return View();
        }

        [AuthenticationAttribute]
        public ActionResult VisitRecordEditStaticAjax()
        {
            var phrase = new DAL.PhraseRepository().Get();
            var product = new DAL.ProductRepository().Get();
            var profession = new DAL.ProfessionRepository().Get();
            var rq = new DAL.RequirementRepository().Get();
            var visitKind = new DAL.VisitKindRepository().Get();
            var returnData = new
            {
                Phrase = phrase,
                Product = product,
                Profession = profession,
                Rq = rq,
                VisitKind = visitKind,
            };
            var data = new ReturnData<Object>()
            {
                Data = returnData,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitRecordEditDataAjax(int Id)
        {
            var returnData = new DAL.VisitReportRepository().GetVisitReport(Id, base.UserId);
            var data = new ReturnData<VisitReportModel>()
            {
                Data = returnData,
                ErrorMessage = "成功",
                ReturnType = ReturnType.Success,
                WarnMessage = "成功",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitRecordEditUpdateAjax(VisitReportUpDateModel input)
        {
            var returnData = new DAL.VisitReportRepository().UpdateVisitReport(input);
            var data = new ReturnData<bool>()
            {
                Data = returnData,
                ErrorMessage = returnData ? "成功" : "失败",
                ReturnType = returnData ? ReturnType.Success : ReturnType.Fail,
                WarnMessage = returnData ? "成功" : "失败",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult VisitRecordEditUpdateCheckAjax(int Id)
        {
            var data = new ReturnData<bool>()
            {
                Data = false,
                ErrorMessage = "失败",
                ReturnType = ReturnType.Fail,
                WarnMessage = "不允许修改",
            };
            var time = new DAL.VisitReportRepository().GetVisitReportTime(Id);
            if (time != null)
            {
                var startTime = new DateTime(time.Value.Year, time.Value.Month, time.Value.Day);
                var endTime = new DateTime(time.Value.Year, time.Value.Month, time.Value.Day + 1);
                if (DateTime.Now >= startTime && DateTime.Now < endTime)
                {
                    data.ReturnType = ReturnType.Success;
                    data.Data = true;
                    data.ErrorMessage = "成功";
                    data.WarnMessage = "成功";
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AuthenticationAttribute]
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

        public ActionResult CancelLogin()
        {
            Class.Tools.CookieHelper.ClearCookie();
            var data = new ReturnData<bool>()
            {
                Data = true,
                ErrorMessage = "成功",
                WarnMessage = "成功",
                ReturnType = ReturnType.Success
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
