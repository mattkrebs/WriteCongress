using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    public class IssuesController : Controller
    {
        //
        // GET: /Issues/
        WriteCongressConnection db = new WriteCongressConnection();
        public ActionResult Index()
        {
            return View(db.Issues.ToList());
        }

    }
}
