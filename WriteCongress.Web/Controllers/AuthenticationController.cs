using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FacebookLogin() {
            return View();
        }
        public ActionResult FacebookChannel() {
            return View();
        }

    }
}
