using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;

namespace WriteCongress.Web.Admin.Controllers
{
    public class IssueController : Controller
    {
        private WriteCongressConnection db = new WriteCongressConnection();

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            return View(db.Issues.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ActionResult Details(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}