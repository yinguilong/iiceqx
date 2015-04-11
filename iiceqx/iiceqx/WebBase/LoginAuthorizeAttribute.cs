using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iiceqx.web.WebBase
{
    public class LoginAuthorizeAttribute : ActionFilterAttribute
    {
        private int[] arrLoginSystemLeves;
        public LoginAuthorizeAttribute(int[] arrLogins)
        {
            this.arrLoginSystemLeves = arrLogins;

        }
        public LoginAuthorizeAttribute()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var currentOperaor = CustomUserSession.Get();
            string strOriginalUrl = string.Format("http://{0}{1}", filterContext.HttpContext.Request.GetServerHostHeader(), ReadonlyCollection.ROOTDOMAIN);
            if (currentOperaor == null)
            {
                filterContext.Result = MyResult;
                return;
            }
            if (arrLoginSystemLeves != null && arrLoginSystemLeves.Any())
            {
                for (int i = 0; i < arrLoginSystemLeves.Length; i++)
                {
                    if (arrLoginSystemLeves[i] == (int)currentOperaor.SystemLevel)
                    {
                        return;
                    }
                }
                filterContext.Result = MyResult;
            }
            return;
        }
        private ContentResult MyResult
        {
            get
            {
                var url = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToLower();
                var host = url.Split('.');
                var contentResult = new ContentResult
                {
                    Content = string.Format("<script>alert('{0}');window.parent.location.href='{1}'</script>", "登录超时了，重新登陆试试？", string.Format("http://{0}{1}", host[0], ReadonlyCollection.ROOTDOMAIN))
                };
                return contentResult;
            }
        }
    }
}