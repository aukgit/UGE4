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
    public class QuestionController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Admin/Question/

        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.MCQ);
            return View(questions.ToList());
        }

        //
        // GET: /Admin/Question/Details/5

        public ActionResult Details(long id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Admin/Question/Create

        public ActionResult Create()
        {
            ViewBag.MCQID = new SelectList(db.MCQs, "MCQID", "Title");
            return View();
        }

        //
        // POST: /Admin/Question/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MCQID = new SelectList(db.MCQs, "MCQID", "Title", question.MCQID);
            return View(question);
        }

        //
        // GET: /Admin/Question/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.MCQID = new SelectList(db.MCQs, "MCQID", "Title", question.MCQID);
            return View(question);
        }

        //
        // POST: /Admin/Question/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MCQID = new SelectList(db.MCQs, "MCQID", "Title", question.MCQID);
            return View(question);
        }

        //
        // GET: /Admin/Question/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Admin/Question/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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