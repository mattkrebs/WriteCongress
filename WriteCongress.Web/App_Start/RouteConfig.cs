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

            routes.MapRoute("Print", "Print/PrintView/{orderItemGuid}", new { controller = "Print", action = "PrintView" });
            routes.MapRoute("About", "about", new { controller = "Home", action = "About" });
            routes.MapRoute("Contact", "contact-us", new { controller = "Home", action = "Contact" });
            routes.MapRoute("SendUsLetters", "send-us-letters", new { controller = "Home", action = "SendUsLetters" });
            routes.MapRoute("HowThisWorks", "how-this-works", new { controller = "Home", action = "HowThisWorks" });
            routes.MapRoute("Privacy", "privacy", new { controller = "Home", action = "Privacy" });
            routes.MapRoute("TOS", "tos", new { controller = "Home", action = "TOS" });
            routes.MapRoute("FAQ", "FAQ", new { controller = "Home", Action = "FAQ" });
            
            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }
    }
}