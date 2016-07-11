using ShunFengCRM.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShunFengCRM.UI.Controllers
{
    public class BaseController : Controller
    {

        public int UserId { get; set; }
        public UserType UserType { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.UserId = ShunFengCRM.UI.Class.Tools.CookieHelper.GetCookie("userId");
            if (this.UserId != 0)
                this.UserType = new DAL.UserInfoRepository().GetUserInfo(this.UserId).UserType;
            base.Initialize(requestContext);
        }
    }
}
