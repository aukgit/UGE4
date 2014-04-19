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
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(MCQ mcq = null){
			var articles = db.Articles.Select(m => new { ArticleID = m.ArticleID , ArticleName = m.ArticleName} ).ToList();

			if(mcq == null) {	
				ViewBag.ArticleID = new SelectList(articles, "ArticleID", "ArticleName");
			} else {
				ViewBag.ArticleID = new SelectList(articles, "ArticleID", "ArticleName", mcq.ArticleID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var mcqs = db.MCQs.Include(m => m.Article).ToList();
			return View(mcqs);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(MCQ mcq)
		{
			bool result = ViewTapping(ViewStates.CreatePost,mcq);
			
			if (ModelState.IsValid)
			{
				db.MCQs.Add(mcq);
				bool state = SaveDatabase(ViewStates.Create, mcq);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(mcq);
			return View(mcq);
		}

		public ActionResult Edit(int id = 0)
		{
			var mcq = db.MCQs.Find(id);
			if (mcq == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(mcq);
			bool result = ViewTapping(ViewStates.Edit,mcq);
			return View(mcq);
		}

		[HttpPost]
		public ActionResult Edit(MCQ mcq)
		{
			bool result = ViewTapping(ViewStates.EditPost,mcq);

			if (ModelState.IsValid)
			{
				db.Entry(mcq).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, mcq);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(mcq);
			return View(mcq);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var mcq = db.MCQs.Find(id);
			if(mcq != null){
				bool result = ViewTapping(ViewStates.DeletePost,mcq);
				db.MCQs.Remove(mcq);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, mcq);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(int id = 0)
		{

			var mcq = db.MCQs.Find(id);
	
			if (mcq == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(mcq);
			bool result = ViewTapping(ViewStates.Details, mcq);
			return View(mcq);
		}

		bool ViewTapping(ViewStates view, MCQ mcq = null){
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

		bool SaveDatabase(ViewStates view, MCQ mcq = null){
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