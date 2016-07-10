using ShunFengCRM.UI.Class;
using ShunFengCRM.UI.Class.Tools;
using ShunFengCRM.UI.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult PersonalEdit()
        {
            return View();
        }


        public ActionResult PersonalEditAjax()
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
            };
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
