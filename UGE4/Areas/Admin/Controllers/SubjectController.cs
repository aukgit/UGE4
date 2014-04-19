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
	public class SubjectController : Controller
	{
        readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Subject subject = null){

			if(subject == null) {	
			} else {
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			return View(db.Subjects.ToList());
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(Subject subject)
		{
			bool result = ViewTapping(ViewStates.CreatePost,subject);
			
			if (ModelState.IsValid)
			{
				db.Subjects.Add(subject);
				bool state = SaveDatabase(ViewStates.Create, subject);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(subject);
			return View(subject);
		}

		public ActionResult Edit(byte id = 0)
		{
			var subject = db.Subjects.Find(id);
			if (subject == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(subject);
			bool result = ViewTapping(ViewStates.Edit,subject);
			return View(subject);
		}

		[HttpPost]
		public ActionResult Edit(Subject subject)
		{
			bool result = ViewTapping(ViewStates.EditPost,subject);

			if (ModelState.IsValid)
			{
				db.Entry(subject).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, subject);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(subject);
			return View(subject);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(byte id)
		{
			var subject = db.Subjects.Find(id);
			if(subject != null){
				bool result = ViewTapping(ViewStates.DeletePost,subject);
				db.Subjects.Remove(subject);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, subject);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(byte id = 0)
		{

			var subject = db.Subjects.Find(id);
	
			if (subject == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(subject);
			bool result = ViewTapping(ViewStates.Details, subject);
			return View(subject);
		}

		bool ViewTapping(ViewStates view, Subject subject = null){
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

		bool SaveDatabase(ViewStates view, Subject subject = null){
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