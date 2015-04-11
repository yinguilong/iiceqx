using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iiceqx.web.WebBase
{
    public static class HttpContextExtensions
    {
        public static string GetServerHostHeader(this HttpRequestBase request)
        {
            var url = request.ServerVariables["HTTP_HOST"].ToLower();
            var host = url.Split('.');
            return host[0];
        }

        public static void Expired(this HttpCookieCollection cookies, string name)
        {
            var cookie = cookies[name];
            if (cookie == null) return;

            cookie.HttpOnly = true;
            cookie.Domain = ReadonlyCollection.ROOTDOMAIN;
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookies.Add(cookie);
        }
        /// <summary>
        /// 修正版
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void Add(this HttpCookieCollection cookies, string name, string value, DateTime? expires)
        {
            if (cookies[name] == null)
            {
                var cookie = new HttpCookie(name, value)
                {
                    Domain = ReadonlyCollection.ROOTDOMAIN.TrimStart('.'),
                    HttpOnly = false
                };
                if (expires.HasValue)
                    cookie.Expires = expires.Value;
                cookies.Add(cookie);
            }
            else
            {
                var cookie = cookies[name];
                cookie.Domain = ReadonlyCollection.ROOTDOMAIN;
                cookie.HttpOnly = false;
                cookie.Value = value;
                if (expires.HasValue)
                    cookie.Expires = expires.Value;
            }


        }
        /// <summary>
        /// 加强版 按键值添加
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="rootDomain"></param>
        /// <param name="expires"></param>
        public static void Add(this HttpCookieCollection cookies, string name, string valueKey, string valueVlues)
        {
            var cookie = new HttpCookie(name);
            cookie.HttpOnly = true;
            cookie[valueKey] = valueVlues;
            cookie.Domain = ReadonlyCollection.ROOTDOMAIN;
            cookie.Expires = DateTime.Now.AddHours(6);
            cookies.Add(cookie);
        }
        /// <summary>
        /// 指定二级域名有效cookie
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="rootDomain"></param>
        /// <param name="expires"></param>
        public static void Add(this HttpCookieCollection cookies, string name, string value, string rootDomain, DateTime? expires)
        {
            var cookie = new HttpCookie(name, value)
            {
                Domain = rootDomain,
                HttpOnly = true
            };
            if (expires.HasValue)
                cookie.Expires = expires.Value;
            cookies.Add(cookie);
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        #region 获取客户端真实IP
        public static string ClientIP(this Controller ctrl)
        {
            string userip = "";
            // 如果客户端用了代理服务器，则应该用ServerVariables("HTTP_X_FORWARDED_FOR")方法
            if (ctrl.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                userip = ctrl.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            // 如果客户端没用代理，应该用Request.ServerVariables("REMOTE_ADDR")方法
            if (userip.Length == 0)
                userip = ctrl.HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString();
            return userip;
        }
        #endregion



        /// <summary>
        /// 取Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region 取Cookie值
        public static string GetCookieValue(this Controller ctrl, string key)
        {
            HttpCookie cookie = ctrl.HttpContext.Request.Cookies[key];
            if (cookie == null)
                return string.Empty;
            return cookie.Value;
        }
        #endregion

        /// <summary>
        /// 存Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        #region 存Cookie
        public static void SetCookie(this Controller ctrl, string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            ctrl.HttpContext.Response.Cookies.Add(cookie);
        }

        public static void SetCookie(this Controller ctrl, string key, string value, TimeSpan ts)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Expires = DateTime.Now.Add(ts);
            ctrl.HttpContext.Response.Cookies.Add(cookie);
        }
    }
}
        #endregion