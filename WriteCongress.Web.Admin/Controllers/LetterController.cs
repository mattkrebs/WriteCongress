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
    public class LetterController : Controller
    {
        private WriteCongressEntities db = new WriteCongressEntities();

        //
        // GET: /Letter/

        public ActionResult Index()
        {
            var letters = db.Letters.Include(l => l.Issue);
            return View(letters.ToList());
        }

        //
        // GET: /Letter/Details/5

        public ActionResult Details(int id = 0)
        {
            Letter letter = db.Letters.Find(id);
            if (letter == null)
            {
                return HttpNotFound();
            }
            return View(letter);
        }

        //
        // GET: /Letter/Create

        public ActionResult Create()
        {
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Name");
            return View();
        }

        //
        // POST: /Letter/Create

        [HttpPost]
        public ActionResult Create(Letter letter)
        {
            if (ModelState.IsValid)
            {
                db.Letters.Add(letter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Name", letter.IssueId);
            return View(letter);
        }

        //
        // GET: /Letter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Letter letter = db.Letters.Find(id);
            if (letter == null)
            {
                return HttpNotFound();
            }
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Name", letter.IssueId);
            return View(letter);
        }

        //
        // POST: /Letter/Edit/5

        [HttpPost]
        public ActionResult Edit(Letter letter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(letter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Name", letter.IssueId);
            return View(letter);
        }

        //
        // GET: /Letter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Letter letter = db.Letters.Find(id);
            if (letter == null)
            {
                return HttpNotFound();
            }
            return View(letter);
        }

        //
        // POST: /Letter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Letter letter = db.Letters.Find(id);
            db.Letters.Remove(letter);
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