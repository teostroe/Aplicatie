using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicentaApp;
using System.Data.Entity.Core.Objects;
using LicentaApp.Controllers.Base;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels.Clienti;
using LicentaApp.ViewModels.Comanda;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ClientiController : BaseAppController
    {
        // GET: Clienti
        public ActionResult Index(int? page)
        {
            var model = db.Clienti.ToList();
            ViewData.InitializePagination(page, model.Count, this.ControllerContext);
            return View(db.Clienti.ToList());
        }

        // GET: Clienti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Clienti
                .Where(x => x.Id == id)
                .Select(x => new ClientReadOneViewModel()
                {
                    Client = x,
                    ViziteMedicale = x.Comenzi.Where(y => y.ViziteMedicale != null)
                        .Select(y => new ViziteMedicaleReadOneViewModel
                        {
                            VizitaMedicala = y.ViziteMedicale,
                            Data = y.Data
                        })

                }).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Clienti/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Action = AppConstants.CRUD.Edit;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clienti clienti)
        {
            if (!ModelState.IsValid)
            {
                return View(clienti);
            }

            var dbClienti = this.db.Clienti.Single(x => x.Id == clienti.Id);
            db.Clienti.Attach(dbClienti);
            dbClienti.DataNastere = clienti.DataNastere;
            dbClienti.Nume = clienti.Nume;
            dbClienti.Prenume = clienti.Prenume;
            dbClienti.Email = clienti.Email;
            dbClienti.NumarTelefon = clienti.NumarTelefon;
            dbClienti.Profesie = clienti.Profesie;

            var result = this.SaveChages();
            if (result != DbSaveResult.Success)
            {
                return View(clienti);
            }
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
