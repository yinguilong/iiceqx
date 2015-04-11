using iiceqx.Model;
using iiceqx.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iiceqx.web.WebBase
{
    public class CustomUserSession
    {
        private static readonly object LockObj = new object();

        private static string CurrentHostHeader
        {
            get
            {
                var url = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToLower();
                var host = url.Split('.');
                return host[0];
            }
        }

        public static string SessionId
        {
            get
            {


                if (HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]) && Validate(HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]))
                {
                    return HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE];
                }
                for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
                {
                    var matchCookie = HttpContext.Current.Request.Cookies[i];
                    if (matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE] != null && Validate(matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]))
                    {
                        return matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE];
                    }

                }
                return "";
            }
        }
        public static string GetUserCookie()
        {
            if (HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER_ID] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER_ID][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]) && Validate(HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER_ID][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]))
            {
                return HttpContext.Current.Request.Cookies[ReadonlyCollection.CURRENTUSER_ID][ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE];
            }
            for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
            {
                var matchCookie = HttpContext.Current.Request.Cookies[i];
                if (matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE] != null && Validate(matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE]))
                {
                    return matchCookie[ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE];
                }

            }
            return "";
        }
        public static CurrentOperator Get()
        {
            string key = ReadonlyCollection.CURRENTUSER + SessionId;
            return CacheHelper.Get<CurrentOperator>(key);
        }

        public static void SetCurrUser(CurrentOperator value)
        {
            string key = ReadonlyCollection.CURRENTUSER + SessionId;
            CacheHelper.Set(key, value);
        }

        public static string SaveSessionForLogin(CurrentOperator value)
        {

            if (value == null) return "";
            try
            {
                string sessionId = CreateSessionId();
                string key = ReadonlyCollection.CURRENTUSER + sessionId;
                var expiry = DateTime.Now.AddHours(6);
                CacheHelper.Set(key, value, expiry);
                CacheHelper.Set(value.UserId.ToString(), key);
                HttpContext.Current.Response.Cookies.Add(ReadonlyCollection.CURRENTUSER_ID, ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE, value.UserId.ToString());
                HttpContext.Current.Response.Cookies.Add(ReadonlyCollection.CURRENTUSER, ReadonlyCollection.CLIENTSESSIONNAMECOOKIEVALUE, sessionId);
                return sessionId;
            }
            catch (Exception exp)
            {
                string fileName = string.Format(@"Login_Error_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                Logger.WriteFileLog(fileName, "登录异常", exp.ToString());
                return "";
            }
        }

        private static string GetDomainBySeesionID(string sessionID)
        {
            if (!String.IsNullOrEmpty(sessionID))
            {
                string[] arr = sessionID.Split('^');
                return arr.Length > 1 ? sessionID.Split('^')[1] : "";
            }
            else
                return "";
        }

        public static void Clear()
        {
            var cookie = HttpContext.Current.Response.Cookies[ReadonlyCollection.CLIENTSESSIONNAME];
            if (cookie != null)
            {
                HttpContext.Current.Response.Cookies.Expired(ReadonlyCollection.CLIENTSESSIONNAME);
                HttpContext.Current.Session[ReadonlyCollection.CLIENTSESSIONNAME] = null;
                string key = ReadonlyCollection.SERVICE_KEY_PREFFIX + cookie.ToString();
                CacheHelper.DeleteCacheByKey(key);
            }
        }

        public static string CreateSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool Validate(string id)
        {
            try
            {
                Guid testGuid;
                if (!Guid.TryParse(id, out testGuid))
                    return false;

                if (id == testGuid.ToString())
                    return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(typeof(CustomUserSession), ex);
            }

            return false;
        }
    }
}