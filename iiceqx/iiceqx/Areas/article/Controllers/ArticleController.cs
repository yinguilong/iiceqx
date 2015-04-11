using iiceqx.IBll;
using iiceqx.Tool.IocHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iiceqx.web.WebBase;

namespace iiceqx.web.Areas.article.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /article/Article/
        private IArticleBll articleBll = IocHelper.Resolve<IArticleBll>();
        public ActionResult Index(string themeName, int id = 1)
        {
            //themeName = HttpUtility.UrlDecode(themeName);
            ViewData["pageIndex"] = id;
            var list = articleBll.GetArticleListIndex(themeName, null, id, 5);
            if (list == null)
            {
                return Content("");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Index", list);
            }
            return View(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Article(int? id)
        {
            var article = articleBll.GetArticleById(id ?? 0);
            return View(article);
        }
        public ActionResult articletheme(int id = 1)
        {
            var list = articleBll.GetArticleTheme(id, 10);
            ViewData["pageIndex"] = id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_articletheme", list);
            }
            return View(list);
        }
        /// <summary>
        /// 分类型学习
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="themeId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult articlestudybytheme(string themeName, int? themeId, int id = 1)
        {
            var list = articleBll.GetArticleList(themeName, null);
            if (list == null)
            {
                return Content("");
            }
            var count = list.Count;
            if (id > count)
            {
                return AlertDiv("没有数据了！");
            }
            var model = list[id - 1];
            ViewData["list"] = list;
            ViewData["id"] = id;
            ViewData["theme"] = themeName;
            return View(model);
        }
        public ActionResult articlelistbytheme(string themeName, int id = 1)
        {
            ViewData["pageIndex"] = id;
            ViewData["theme"] = themeName;
            var list = articleBll.GetArticleListIndex(themeName, null, id, 5);
            if (list == null)
            {
                return AlertDiv("没有数据了");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Index", list);
            }
            return View(list);
        }
    }
}
