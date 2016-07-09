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

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.UserId = ShunFengCRM.UI.Class.Tools.CookieHelper.GetCookie("userId");
            base.Initialize(requestContext);
        }
    }
}
