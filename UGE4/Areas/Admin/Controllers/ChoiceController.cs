using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UGE4.Models;

namespace UGE4.Areas.Admin.Controllers
{
    public class ChoiceController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Admin/Choice/

        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.Question);
            return View(choices.ToList());
        }

        //
        // GET: /Admin/Choice/Details/5

        public ActionResult Details(long id = 0)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        //
        // GET: /Admin/Choice/Create

        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Hint");
            return View();
        }

        //
        // POST: /Admin/Choice/Create

        [HttpPost]
        public ActionResult Create(Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Hint", choice.QuestionID);
            return View(choice);
        }

        //
        // GET: /Admin/Choice/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Hint", choice.QuestionID);
            return View(choice);
        }

        //
        // POST: /Admin/Choice/Edit/5

        [HttpPost]
        public ActionResult Edit(Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Hint", choice.QuestionID);
            return View(choice);
        }

        //
        // GET: /Admin/Choice/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        //
        // POST: /Admin/Choice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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