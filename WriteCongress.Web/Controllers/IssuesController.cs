using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    public class IssuesController : BaseController
    {
        public ActionResult Index() {
            var issues = Db.Issues;
            return View(issues.Where(x => x.Active == true).ToList());
        }
        public ActionResult Details(string slug) {
            var issue = Db.Issues.SingleOrDefault(i => i.Slug == slug);
            
            if (issue == null) {
                Logger.Warn("unknown issue slug: {0}", slug);
                return View("CreateIssue");
            }

            ViewBag.Letters = issue.IssueLetters.Select(l => l.Letter).ToList();
            
            return View("Issue",issue);
        }
        public ActionResult IssueLetter(string issueSlug, string letterSlug) {
            
            var letter = Db.IssueLetters.Where(il => il.Issue.Slug == issueSlug && il.Letter.Slug == letterSlug).Select(l => l.Letter).SingleOrDefault();
            if (letter == null) {
                Logger.Warn("no issue letter found. issue:{0}, letter:", issueSlug,letterSlug);
                return RedirectToAction("Index", new {slug = issueSlug});
            }

            IssueLetterViewModel model = new IssueLetterViewModel();
            if (this.AuthenticatedUser != null) {
                model.FirstName = AuthenticatedUser.FirstName;
                model.LastName = AuthenticatedUser.LastName;
                model.Address1 = AuthenticatedUser.AddressOne;
                model.Address2 = AuthenticatedUser.AddressTwo;
                model.City = AuthenticatedUser.City;
                model.State = AuthenticatedUser.State;
                model.PhoneNumber = AuthenticatedUser.PhoneNumber;
                model.Email = AuthenticatedUser.Email;
                model.ZipCode = AuthenticatedUser.ZipCode;
                model.CongressionalDistrict = AuthenticatedUser.CongressionalDistrict ?? -1;
            }
            model.Letter = letter;
            model.Issue =Db.Issues.FirstOrDefault(i => i.Slug == issueSlug);
            return View("IssueLetter", model);
        }

    }
}
