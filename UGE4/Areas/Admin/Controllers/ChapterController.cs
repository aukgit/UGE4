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
	public class ChapterController : Controller
	{
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Chapter chapter = null){
			var articles = db.Articles.Select(c => new { ArticleID = c.ArticleID , ArticleName = c.ArticleName} ).ToList();
			var books = db.Books.Select(c => new { BookID = c.BookID , BookName = c.BookName} ).ToList();

			if(chapter == null) {	
				ViewBag.MasterArticleID = new SelectList(articles, "ArticleID", "ArticleName");
				ViewBag.BookID = new SelectList(books, "BookID", "BookName");
			} else {
				ViewBag.MasterArticleID = new SelectList(articles, "ArticleID", "ArticleName", chapter.MasterArticleID);
				ViewBag.BookID = new SelectList(books, "BookID", "BookName", chapter.BookID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var chapters = db.Chapters.Include(c => c.Article).Include(c => c.Book).ToList();
			return View(chapters);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(Chapter chapter)
		{
			bool result = ViewTapping(ViewStates.CreatePost,chapter);
			
			if (ModelState.IsValid)
			{
				db.Chapters.Add(chapter);
				bool state = SaveDatabase(ViewStates.Create, chapter);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(chapter);
			return View(chapter);
		}

		public ActionResult Edit(int id = 0)
		{
			var chapter = db.Chapters.Find(id);
			if (chapter == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(chapter);
			bool result = ViewTapping(ViewStates.Edit,chapter);
			return View(chapter);
		}

		[HttpPost]
		public ActionResult Edit(Chapter chapter)
		{
			bool result = ViewTapping(ViewStates.EditPost,chapter);

			if (ModelState.IsValid)
			{
				db.Entry(chapter).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, chapter);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(chapter);
			return View(chapter);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var chapter = db.Chapters.Find(id);
			if(chapter != null){
				bool result = ViewTapping(ViewStates.DeletePost,chapter);
				db.Chapters.Remove(chapter);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, chapter);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(int id = 0)
		{

			var chapter = db.Chapters.Find(id);
	
			if (chapter == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(chapter);
			bool result = ViewTapping(ViewStates.Details, chapter);
			return View(chapter);
		}

		bool ViewTapping(ViewStates view, Chapter chapter = null){
			switch (view){
				case ViewStates.Index:
					break;
				case ViewStates.Create:
					break;
				case ViewStates.CreatePost:
					break;
				case ViewStates.Edit:
					break;
				case ViewStates.EditPost:
					break;
				case ViewStates.Delete:
					break;
			}
			return true;
		}

		enum ViewStates{
			Index,
			Create,
			CreatePost,
			Edit,
			EditPost,
			Details,
			Delete,
			DeletePost
		}

		bool SaveDatabase(ViewStates view, Chapter chapter = null){
			// working those at HttpPost time.
			switch (view){
				case ViewStates.Create:
					break;
				case ViewStates.Edit:
					break;
				case ViewStates.Delete:
					break;
			}

			try	{
				var changes = db.SaveChanges();
				if(changes > 0){
					return true;
				}
			} catch (Exception ex){
				 throw new Exception("Message : " + ex.Message.ToString() + " Inner Message : " + ex.InnerException.Message.ToString());
			}
			return false;
		}


		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}