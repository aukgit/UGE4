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
	public class ArticleController : Controller
	{
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Article article = null){
			var chapters = db.Chapters.Select(a => new { ChapterID = a.ChapterID , TopicName = a.TopicName} ).ToList();

			if(article == null) {	
				ViewBag.ChapterID = new SelectList(chapters, "ChapterID", "TopicName");
			} else {
				ViewBag.ChapterID = new SelectList(chapters, "ChapterID", "TopicName", article.ChapterID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var articles = db.Articles.Include(a => a.Chapter).ToList();
			return View(articles);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
		public ActionResult Create(Article article)
		{
			bool result = ViewTapping(ViewStates.CreatePost,article);
			
			if (ModelState.IsValid)
			{
				db.Articles.Add(article);
				bool state = SaveDatabase(ViewStates.Create, article);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(article);
			return View(article);
		}

		public ActionResult Edit(long id = 0)
		{
			var article = db.Articles.Find(id);
			if (article == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(article);
			bool result = ViewTapping(ViewStates.Edit,article);
			return View(article);
		}
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
		public ActionResult Edit(Article article)
		{
			bool result = ViewTapping(ViewStates.EditPost,article);

			if (ModelState.IsValid)
			{
				db.Entry(article).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, article);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(article);
			return View(article);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(long id)
		{
			var article = db.Articles.Find(id);
			if(article != null){
				bool result = ViewTapping(ViewStates.DeletePost,article);
				db.Articles.Remove(article);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, article);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(long id = 0)
		{

			var article = db.Articles.Find(id);
	
			if (article == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(article);
			bool result = ViewTapping(ViewStates.Details, article);
			return View(article);
		}

		bool ViewTapping(ViewStates view, Article article = null){
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

		bool SaveDatabase(ViewStates view, Article article = null){
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