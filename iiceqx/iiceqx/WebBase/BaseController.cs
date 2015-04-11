using iiceqx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iiceqx.web.WebBase
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录帐号
        /// </summary>
        public CurrentOperator CurrentOperator
        {
            get
            {
                return Get();

            }
        }
        private CurrentOperator Get()
        {
            return CustomUserSession.Get();
        }
        /// <summary>
        /// 保存登录账户信息
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        protected string LoginUser(CurrentOperator userLogin)
        {

            return CustomUserSession.SaveSessionForLogin(userLogin);
        }

        /// <summary>
        /// 用户退出登录
        /// </summary>
        protected void LogoutUser()
        {
            CustomUserSession.Clear();
        }
        /// <summary>
        /// 当前二级域名
        /// </summary>
        protected string CurrentHostHeader
        {
            get
            {

                //return "libr";

                return this.HttpContext.Request.GetServerHostHeader();
            }
        }
        /// <summary>
        /// 弹框提示
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ActionResult Alert(string content)
        {
            var contentResult = new ContentResult
            {
                Content = string.Format("<script>alert('{0}');window.history.back();</script>", content)
            };
            return contentResult;
        }
        /// <summary>
        /// bootstrap警告提示
        /// </summary>
        /// <param name="content"></param>
        /// <param name="directUrl"></param>
        /// <returns></returns>
        public ActionResult AlertDiv(string content, string directUrl = "")
        {
            return Redirect(string.Format("/Home/error?content={0}&directUrl={1}", content, directUrl));
        }
        /// <summary>
        /// 弹框提示
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="content"></param>
        /// <param name="directURL"> </param>
        /// <returns></returns>
        public ActionResult Alert(string content, string directURL)
        {
            var contentResult = new ContentResult
            {
                Content = string.Format("<script>alert('{0}');window.location.href='{1}'</script>", content, directURL)
            };
            return contentResult;
        }
        public ActionResult AlertParentFrame(string content, string directURL)
        {
            var contentResult = new ContentResult
            {
                Content = string.Format("<script>alert('{0}');window.parent.location.href='{1}'</script>", content, directURL)
            };
            return contentResult;
        }
        public ActionResult AlertTopFrame(string content, string directUrl, string frameName = "manFrame")
        {
            var contentResult = new ContentResult
            {
                Content = string.Format("<script>alert('{0}');window.top.frames['{2}'].location.href='{1}'</script>", content, directUrl, frameName)
            };
            return contentResult;
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        #region 获取客户端真实IP
        [NonAction]
        public string ClientIP()
        {
            string userip = "";
            // 如果客户端用了代理服务器，则应该用ServerVariables("HTTP_X_FORWARDED_FOR")方法
            if (this.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                userip = this.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            // 如果客户端没用代理，应该用Request.ServerVariables("REMOTE_ADDR")方法
            if (userip.Length == 0)
                userip = this.Request.ServerVariables["REMOTE_ADDR"].ToString();
            return userip;
        }
        #endregion
    }
}
