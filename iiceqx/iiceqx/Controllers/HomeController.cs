using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiceqx.IBll;
using iiceqx.Tool.IocHelper;
using iiceqx.Tool;
using log4net;
using iiceqx.web.WebBase;
namespace iiceqx.web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private ICommonBll commonBll = IocHelper.Resolve<ICommonBll>();
        private INewsBll newsBll = IocHelper.Resolve<INewsBll>();
        public ActionResult Index()
        {
            //Logger.WriteFileLog("yinguilong", "sssssss");
            //CacheHelper.Set("iiceqxAdmin", "yinguilong");
            //var boolCheck = commonBll.LoginCheck("yinguilong", "ygllxn1108");
            //ViewData["isCheck"] = boolCheck;
            // var admin = CacheHelper.Get<string>("iiceqxAdmin");
            // Logger.WriteFileLog("ygl", "iiceqx");
            var user = CustomUserSession.GetUserCookie();
            if (!string.IsNullOrEmpty(user))
            {
                var list = newsBll.GetIndexNewsForUser(int.Parse(user), 6);
                ViewData["news"] = list;
            }
            else
            {
                ViewData["news"] = newsBll.GetInexNewsForVisitor(6);
            }
            //ViewData["user"] = admin;
            return View();
        }
        public ActionResult error(string content,string directUrl)
        {
            ViewData["content"] = content;
            if (!string.IsNullOrEmpty(directUrl))
            {
                ViewData["directUrl"] = @"window.location.href='"+directUrl+"';";
            }
            else
            {
                ViewData["directUrl"] = @"window.history.back();";
            }
            return View();
        }

    }
}
