using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShunFengCRM.UI.Class.Tools
{
    public class CookieHelper
    {
        private static readonly string cookieName = "userinfo";


        /// <summary>
        /// 设置用户cookie 赵凯，2016-07-07
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCookie(string key, string value)
        {
            HttpContext context = HttpContext.Current;
            key = key.ToLower();
            value = value.ToLower();
            HttpCookie cookies = context.Request.Cookies[cookieName];
            if (cookies == null)
            {
                cookies = new HttpCookie(cookieName);
                cookies.Values.Add(key, value);
            }
            else
            {
                if (string.IsNullOrEmpty(cookies.Values[key]))
                    cookies.Values.Add(key, value);
                else
                    cookies.Values[key] = value;
            }
            context.Response.Cookies.Add(cookies);
        }

        /// <summary>
        /// 获取用户cookie 赵凯，2016-07-07
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetCookie(string key)
        {
            HttpContext context = HttpContext.Current;
            key = key.ToLower();
            HttpCookie cookies = context.Request.Cookies[cookieName];
            if (cookies == null)
            {
                return 0;
            }
            else
            {
                var userIdStr = cookies.Values[key];
                int userId = 0;
                int.TryParse(userIdStr, out userId);
                return userId;
            }
        }
    }
}