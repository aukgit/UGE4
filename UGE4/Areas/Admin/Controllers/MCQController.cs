using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UGE4.DbInfrastructure;

namespace UGE4.Areas.Admin.Controllers
{
    public class MCQController : Controller
    {
        private UGEContext db = new UGEContext();

        //
        // GET: /Admin/MCQ/

        public ActionResult Index()
        {
            var mcqs = db.MCQs.Include(m => m.Article);
            return View(mcqs.ToList());
        }

        //
        // GET: /Admin/MCQ/Details/5

        public ActionResult Details(int id = 0)
        {
            MCQ mcq = db.MCQs.Find(id);
            if (mcq == null)
            {
                return HttpNotFound();
            }
            return View(mcq);
        }

        //
        // GET: /Admin/MCQ/Create

        public ActionResult Create()
        {
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName");
            return View();
        }

        //
        // POST: /Admin/MCQ/Create

        [HttpPost]
        public ActionResult Create(MCQ mcq)
        {
            if (ModelState.IsValid)
            {
                db.MCQs.Add(mcq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", mcq.ArticleID);
            return View(mcq);
        }

        //
        // GET: /Admin/MCQ/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MCQ mcq = db.MCQs.Find(id);
            if (mcq == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", mcq.ArticleID);
            return View(mcq);
        }

        //
        // POST: /Admin/MCQ/Edit/5

        [HttpPost]
        public ActionResult Edit(MCQ mcq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mcq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", mcq.ArticleID);
            return View(mcq);
        }

        //
        // GET: /Admin/MCQ/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MCQ mcq = db.MCQs.Find(id);
            if (mcq == null)
            {
                return HttpNotFound();
            }
            return View(mcq);
        }

        //
        // POST: /Admin/MCQ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MCQ mcq = db.MCQs.Find(id);
            db.MCQs.Remove(mcq);
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