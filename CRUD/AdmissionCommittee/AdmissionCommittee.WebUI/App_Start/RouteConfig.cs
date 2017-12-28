using System.Web.Mvc;
using System.Web.Routing;

namespace AdmissionCommittee.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{enrolleeId}"
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{edLevel}/{page}",
                defaults: new
                {
                    edLevel = "AnyLevel"
                }
                
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}/{page}"
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}"
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Admin",
                    action = "Index"
                }
            );
            
        }
    }
}
