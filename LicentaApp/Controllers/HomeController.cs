using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LicentaDbContext db = new LicentaDbContext();
            db.FILTREAZAPRODUSE_DENUMIRE("Furla Brigitte");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}