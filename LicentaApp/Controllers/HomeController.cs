using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain.Auth;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = User as AppPrincipal;
            if (user.IsInRole(AuthConstants.Roluri.Utilizator))
            {
                return RedirectToAction("Index", "Comenzi");
            }

            if (user.IsInRole(AuthConstants.Roluri.Admin))
            {
                return RedirectToAction("Index", "Rapoarte");
            }

            return View();
        }

        public ActionResult Admin()
        {
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