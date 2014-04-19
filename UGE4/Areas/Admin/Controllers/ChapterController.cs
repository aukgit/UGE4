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
    public class ChapterController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Admin/Chapter/

        public ActionResult Index()
        {
            var chapters = db.Chapters.Include(c => c.Book);
            return View(chapters.ToList());
        }

        //
        // GET: /Admin/Chapter/Details/5

        public ActionResult Details(int id = 0)
        {
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        //
        // GET: /Admin/Chapter/Create

        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName");
            return View();
        }

        //
        // POST: /Admin/Chapter/Create

        [HttpPost]
        public ActionResult Create(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", chapter.BookID);
            return View(chapter);
        }

        //
        // GET: /Admin/Chapter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", chapter.BookID);
            return View(chapter);
        }

        //
        // POST: /Admin/Chapter/Edit/5

        [HttpPost]
        public ActionResult Edit(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", chapter.BookID);
            return View(chapter);
        }

        //
        // GET: /Admin/Chapter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        //
        // POST: /Admin/Chapter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            db.Chapters.Remove(chapter);
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