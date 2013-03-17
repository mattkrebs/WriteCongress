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
        public ActionResult Index(string id) {
            var issue = db.Issues.SingleOrDefault(i => i.Slug == id);
            if (issue == null)
            {
                return View("CreateIssue");//TODO: create a view to let people create an issue?
            }
            return View("Issue",issue);
        }

    }
}
