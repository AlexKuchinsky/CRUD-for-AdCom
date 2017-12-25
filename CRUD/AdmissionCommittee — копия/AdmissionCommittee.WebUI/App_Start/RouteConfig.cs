using System.Web.Mvc;
using System.Web.Routing;

namespace AdmissionCommittee.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
            (
                null,
                "",
                new { controller = "Admin", action = "Index", page = 1 }
            );

            routes.MapRoute
            (
                null,
                "{controller}/{action}/{enrolleeId}",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute
            (
                null,
                "{controller}/{action}/{edLevel}/{page}",
                new { controller = "Admin", action = "Index", page = UrlParameter.Optional }
            );

            routes.MapRoute
            (
                null,
                "{controller}/{action}/{edLevel}/Page{page}",
                new { controller = "Admin", action = "Index"}
            );

            routes.MapRoute
            (
                null,
                "Page{page}",
                new { controller = "Admin", action = "Index" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
