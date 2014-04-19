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
	public class QuestionController : Controller
	{
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Question question = null){
			var mcqs = db.MCQs.Select(q => new { MCQID = q.MCQID , Title = q.Title} ).ToList();

			if(question == null) {	
				ViewBag.MCQID = new SelectList(mcqs, "MCQID", "Title");
			} else {
				ViewBag.MCQID = new SelectList(mcqs, "MCQID", "Title", question.MCQID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var questions = db.Questions.Include(q => q.MCQ).ToList();
			return View(questions);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(Question question)
		{
			bool result = ViewTapping(ViewStates.CreatePost,question);
			
			if (ModelState.IsValid)
			{
				db.Questions.Add(question);
				bool state = SaveDatabase(ViewStates.Create, question);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(question);
			return View(question);
		}

		public ActionResult Edit(long id = 0)
		{
			var question = db.Questions.Find(id);
			if (question == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(question);
			bool result = ViewTapping(ViewStates.Edit,question);
			return View(question);
		}

		[HttpPost]
		public ActionResult Edit(Question question)
		{
			bool result = ViewTapping(ViewStates.EditPost,question);

			if (ModelState.IsValid)
			{
				db.Entry(question).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, question);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(question);
			return View(question);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(long id)
		{
			var question = db.Questions.Find(id);
			if(question != null){
				bool result = ViewTapping(ViewStates.DeletePost,question);
				db.Questions.Remove(question);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, question);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(long id = 0)
		{

			var question = db.Questions.Find(id);
	
			if (question == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(question);
			bool result = ViewTapping(ViewStates.Details, question);
			return View(question);
		}

		bool ViewTapping(ViewStates view, Question question = null){
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

		bool SaveDatabase(ViewStates view, Question question = null){
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