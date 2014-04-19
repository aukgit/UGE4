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
	public class ChoiceController : Controller
	{
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Choice choice = null){
			var questions = db.Questions.Select(c => new { QuestionID = c.QuestionID , Hint = c.Hint} ).ToList();

			if(choice == null) {	
				ViewBag.QuestionID = new SelectList(questions, "QuestionID", "Hint");
			} else {
				ViewBag.QuestionID = new SelectList(questions, "QuestionID", "Hint", choice.QuestionID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var choices = db.Choices.Include(c => c.Question).ToList();
			return View(choices);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(Choice choice)
		{
			bool result = ViewTapping(ViewStates.CreatePost,choice);
			
			if (ModelState.IsValid)
			{
				db.Choices.Add(choice);
				bool state = SaveDatabase(ViewStates.Create, choice);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(choice);
			return View(choice);
		}

		public ActionResult Edit(long id = 0)
		{
			var choice = db.Choices.Find(id);
			if (choice == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(choice);
			bool result = ViewTapping(ViewStates.Edit,choice);
			return View(choice);
		}

		[HttpPost]
		public ActionResult Edit(Choice choice)
		{
			bool result = ViewTapping(ViewStates.EditPost,choice);

			if (ModelState.IsValid)
			{
				db.Entry(choice).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, choice);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(choice);
			return View(choice);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(long id)
		{
			var choice = db.Choices.Find(id);
			if(choice != null){
				bool result = ViewTapping(ViewStates.DeletePost,choice);
				db.Choices.Remove(choice);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, choice);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(long id = 0)
		{

			var choice = db.Choices.Find(id);
	
			if (choice == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(choice);
			bool result = ViewTapping(ViewStates.Details, choice);
			return View(choice);
		}

		bool ViewTapping(ViewStates view, Choice choice = null){
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

		bool SaveDatabase(ViewStates view, Choice choice = null){
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