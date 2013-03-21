using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WriteCongress.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Issue", "Issues/{slug}", new { controller = "Issues", action = "Details" });
            routes.MapRoute("IssueLetter", "Issues/{issueSlug}/{letterSlug}", new { controller = "Issues", action = "IssueLetter" });
            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            
            
            
        }
    }
}