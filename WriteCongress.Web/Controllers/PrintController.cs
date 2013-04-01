using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriteCongress.Web.Controllers
{
    public class PrintController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintView(string letter)
        {
            ///TODO wire  up real object

            return View();
        }


    }
}
