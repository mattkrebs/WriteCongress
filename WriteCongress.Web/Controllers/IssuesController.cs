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
        public ActionResult Index(string slug) {
            var issue = db.Issues.SingleOrDefault(i => i.Slug == slug);
            
            if (issue == null)
            {
                return View("CreateIssue");//TODO: create a view to let people create an issue?
            }

            ViewBag.Letters = issue.IssueLetters.Select(l => l.Letter).ToList();
            
            return View("Issue",issue);
        }
        public ActionResult IssueLetter(string issueSlug, string letterSlug) {
            
            var letter = db.IssueLetters.Where(il => il.Issue.Slug == issueSlug && il.Letter.Slug == letterSlug).Select(l => l.Letter).SingleOrDefault();
            if (letter == null) {
                return RedirectToAction("Index", new {slug = issueSlug});
            }

            ViewBag.Issue = db.Issues.FirstOrDefault(i => i.Slug == issueSlug);
            return View("IssueLetter", letter);

        }

    }
}
