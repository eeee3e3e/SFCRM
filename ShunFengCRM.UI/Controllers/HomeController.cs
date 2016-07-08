using ShunFengCRM.UI.Class;
using ShunFengCRM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShunFengCRM.UI.Controllers
{
    public class HomeController : Controller
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
            var userInfo = new ShunFengCRM.DAL.UserInfoRepository().GetUser(loginName, password);
            ReturnData<string> data = null;
            if (userInfo != null)
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
                Class.Tools.CookieHelper.SetCookie("userId", userInfo.UserId.ToString());
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

        public ActionResult Advertisement()
        {
            var imgStr = new ShunFengCRM.DAL.AdvertyRepository().GetAdverty();
            ViewBag.GuangGao = imgStr;
            return View();
        }


    }
}
