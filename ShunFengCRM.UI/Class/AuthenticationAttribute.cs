using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShunFengCRM.UI.Class
{

    /// <summary>
    /// 登录验证 赵凯 2016-07-06
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = Tools.CookieHelper.GetCookie("userId");
            if (userId == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
                return;
            }
            if (!new DAL.UserInfoRepository().GetUser(userId))
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}