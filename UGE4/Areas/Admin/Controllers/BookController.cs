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
	public class BookController : Controller
	{
		readonly UGEContext db = new UGEContext();
		
		void GenerateDropdowns(Book book = null){
			var subjects = db.Subjects.Select(b => new { SubjectID = b.SubjectID , SubjectName = b.SubjectName} ).ToList();

			if(book == null) {	
				ViewBag.SubjectID = new SelectList(subjects, "SubjectID", "SubjectName");
			} else {
				ViewBag.SubjectID = new SelectList(subjects, "SubjectID", "SubjectName", book.SubjectID);
			} 
		}

		
		public ActionResult Index() {
			bool result = ViewTapping(ViewStates.Index);

			var books = db.Books.Include(b => b.Subject).ToList();
			return View(books);
		}

		public ActionResult Create() {
			GenerateDropdowns();
			bool result = ViewTapping(ViewStates.Create);
			return View();
		}

		[HttpPost]
		public ActionResult Create(Book book)
		{
			bool result = ViewTapping(ViewStates.CreatePost,book);
			
			if (ModelState.IsValid)
			{
				db.Books.Add(book);
				bool state = SaveDatabase(ViewStates.Create, book);
				return RedirectToActionPermanent("Index");
			}

			GenerateDropdowns(book);
			return View(book);
		}

		public ActionResult Edit(int id = 0)
		{
			var book = db.Books.Find(id);
			if (book == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(book);
			bool result = ViewTapping(ViewStates.Edit,book);
			return View(book);
		}

		[HttpPost]
		public ActionResult Edit(Book book)
		{
			bool result = ViewTapping(ViewStates.EditPost,book);

			if (ModelState.IsValid)
			{
				db.Entry(book).State = EntityState.Modified;
				bool state = SaveDatabase(ViewStates.Edit, book);
				return RedirectToActionPermanent("Index");
			}
			GenerateDropdowns(book);
			return View(book);
		}

		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var book = db.Books.Find(id);
			if(book != null){
				bool result = ViewTapping(ViewStates.DeletePost,book);
				db.Books.Remove(book);
			}
			
			bool state = SaveDatabase(ViewStates.Delete, book);
			return RedirectToActionPermanent("Index");
		}		
		
		public ActionResult Details(int id = 0)
		{

			var book = db.Books.Find(id);
	
			if (book == null)
			{
				return HttpNotFound();
			}
			GenerateDropdowns(book);
			bool result = ViewTapping(ViewStates.Details, book);
			return View(book);
		}

		bool ViewTapping(ViewStates view, Book book = null){
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

		bool SaveDatabase(ViewStates view, Book book = null){
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