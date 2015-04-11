using System.Web.Mvc;

namespace iiceqx.web.Areas.news
{
    public class newsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "news";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "news_default",
                "news/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
