using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Controllers.Base;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels.Utilizatori;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ProfilController : BaseAppController
    {
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

        public ActionResult VizualizeazaProfil()
        {
            var utilizatorCurent = User as AppPrincipal;
            return View(this.db.Utilizatori.Include(x => x.Magazine).Include(x=>x.Roluri).FirstOrDefault(x => x.Id == utilizatorCurent.UserId));
        }

        [HttpGet]
        public ActionResult ModificaParola()
        {
            var utilizatorCurent = User as AppPrincipal;
            var model = this.db.Utilizatori.Where(x => x.Id == utilizatorCurent.UserId).Select(x => new ModificaParolaViewModel
            {
                Username = x.Username
            }).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult ModificaParola(ModificaParolaViewModel model)
        {


            var dbUtilizator = this.db.Utilizatori.First(x => x.Username == model.Username);

            if (model.ParolaCurenta != dbUtilizator.Parola)
            {
                ModelState.AddModelError("", "Parola introdusa nu este corecta");
            }

            if (model.ParolaNoua != model.ConfirmaParolaNoua)
            {
                ModelState.AddModelError("", "Parola noua nu corespunde cu cea confirmata");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.db.Utilizatori.Attach(dbUtilizator);
            dbUtilizator.Parola = model.ParolaNoua;
            var result = this.SaveChanges();

            if (result != DbSaveResult.Success)
            {
                return View(model);
            }
            return RedirectToAction("VizualizeazaProfil");
        }
    }
}