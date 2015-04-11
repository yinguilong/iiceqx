using iiceqx.web.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiceqx.IBll;
using iiceqx.Tool;
using iiceqx.Tool.IocHelper;
namespace iiceqx.web.Areas.news.Controllers
{
    public class newsController : BaseController
    {
        //
        // GET: /news/news/
        private INewsBll newsBll = IocHelper.Resolve<INewsBll>();
        public ActionResult Index(int id = 1)
        {
            //themeName = HttpUtility.UrlDecode(themeName);
            ViewData["pageIndex"] = id;
            var list = newsBll.GetNewsIndexByPage(id, 10);
            if (list == null)
            {
                return AlertDiv("暂时没有新闻,去首页看看？","/Home/Index");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Index", list);
            }
            return View(list);
        }
        public ActionResult NewsReader(int? id)
        {
            var news = newsBll.GetNewsByNewsId(id ?? 0);
            return View(news);
        }
    }
}
