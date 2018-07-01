using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain.Auth;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ProfilController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Utilizatori()
        {
            var utilizatori = db.Utilizatori.Include(u => u.Magazine).Include(u => u.Roluri);
            return PartialView("Utilizatori/Index", utilizatori.ToList());
        }
    }
}