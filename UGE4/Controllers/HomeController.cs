using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UGE4.DbInfrastructure;

namespace UGE4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new UGEContext();
            ViewBag.Message = "University Grade Education System";
            ViewBag.Videos = db.Articles.ToList();
            return View();
        }

    
    }
}
