﻿using ShunFengCRM.UI.Class;
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
            var sorting = new ShunFengCRM.DAL.VisitReportRepository().GetUserRank(UserId, userInfo.UserType, Class.Tools.DateTimeHelper.GetThisWeekMonday(DateTime.Now));
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

        public ActionResult PersonalEdit(string strPassword)
        {
            var strID = Class.Tools.CookieHelper.GetCookie("userId");
            strID = 1;
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().EditUser(strID, strPassword);
            ReturnData<string> data = null;


            if (userInfo)
            {
                //edit successful
                data = new ReturnData<string>
                {
                    Data = "/home/mainfrm.",
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

        public ActionResult StatusReminder()
        {
            return View();
        }
    }
}
