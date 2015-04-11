using System.Web.Mvc;

namespace iiceqx.web.Areas.article
{
    public class articleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "article";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "article_default",
                "article/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
