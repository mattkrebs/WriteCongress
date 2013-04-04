﻿using System;
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
        public ActionResult Index()
        {
            var issues = Db.Issues;
            return View(issues.Where(x => x.Active == true).ToList());
        }
        public ActionResult Details(string slug) {
            var issue = Db.Issues.SingleOrDefault(i => i.Slug == slug);
            
            if (issue == null)
            {
                return View("CreateIssue");//TODO: create a view to let people create an issue?
            }

            ViewBag.Letters = issue.IssueLetters.Select(l => l.Letter).ToList();
            
            return View("Issue",issue);
        }
        public ActionResult IssueLetter(string issueSlug, string letterSlug) {
            
            var letter = Db.IssueLetters.Where(il => il.Issue.Slug == issueSlug && il.Letter.Slug == letterSlug).Select(l => l.Letter).SingleOrDefault();
            if (letter == null) {
                
                



                return RedirectToAction("Index", new {slug = issueSlug});
            }
            LetterModel letterModel = new LetterModel()
            {
                Letter = letter,
                User = this.AuthenticatedUser
                
            };

            

            ViewBag.Issue = Db.Issues.FirstOrDefault(i => i.Slug == issueSlug);
            return View("IssueLetter", letterModel);

        }

    }
}
