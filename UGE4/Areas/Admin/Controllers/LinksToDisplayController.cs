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
    public class LinksToDisplayController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Admin/LinksToDisplay/

        public ActionResult Index()
        {
            var linkstodisplays = db.LinksToDisplays.Include(l => l.Article).Include(l => l.Chapter);
            return View(linkstodisplays.ToList());
        }

        //
        // GET: /Admin/LinksToDisplay/Details/5

        public ActionResult Details(long id = 0)
        {
            LinksToDisplay linkstodisplay = db.LinksToDisplays.Find(id);
            if (linkstodisplay == null)
            {
                return HttpNotFound();
            }
            return View(linkstodisplay);
        }

        //
        // GET: /Admin/LinksToDisplay/Create

        public ActionResult Create()
        {
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName");
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "TopicName");
            return View();
        }

        //
        // POST: /Admin/LinksToDisplay/Create

        [HttpPost]
        public ActionResult Create(LinksToDisplay linkstodisplay)
        {
            if (ModelState.IsValid)
            {
                db.LinksToDisplays.Add(linkstodisplay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", linkstodisplay.ArticleID);
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "TopicName", linkstodisplay.ChapterID);
            return View(linkstodisplay);
        }

        //
        // GET: /Admin/LinksToDisplay/Edit/5

        public ActionResult Edit(long id = 0)
        {
            LinksToDisplay linkstodisplay = db.LinksToDisplays.Find(id);
            if (linkstodisplay == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", linkstodisplay.ArticleID);
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "TopicName", linkstodisplay.ChapterID);
            return View(linkstodisplay);
        }

        //
        // POST: /Admin/LinksToDisplay/Edit/5

        [HttpPost]
        public ActionResult Edit(LinksToDisplay linkstodisplay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkstodisplay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", linkstodisplay.ArticleID);
            ViewBag.ChapterID = new SelectList(db.Chapters, "ChapterID", "TopicName", linkstodisplay.ChapterID);
            return View(linkstodisplay);
        }

        //
        // GET: /Admin/LinksToDisplay/Delete/5

        public ActionResult Delete(long id = 0)
        {
            LinksToDisplay linkstodisplay = db.LinksToDisplays.Find(id);
            if (linkstodisplay == null)
            {
                return HttpNotFound();
            }
            return View(linkstodisplay);
        }

        //
        // POST: /Admin/LinksToDisplay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            LinksToDisplay linkstodisplay = db.LinksToDisplays.Find(id);
            db.LinksToDisplays.Remove(linkstodisplay);
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