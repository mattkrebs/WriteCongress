using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriteCongress.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static string RenderPartialViewToString(this Controller c, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {

                viewName = c.RouteData.GetRequiredString("action");
            }

            c.ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(c.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(c.ControllerContext, viewResult.View, c.ViewData, c.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}