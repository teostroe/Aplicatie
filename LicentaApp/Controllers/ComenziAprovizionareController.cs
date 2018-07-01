using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Services.ValidationServices.Implementations;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels.ComandaAprovizionare;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ComenziAprovizionareController : Controller
    {

        private LicentaDbContext db = new LicentaDbContext();


        #region De la Furnizor
        [HttpGet]
        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        public ActionResult ComenziAprovizionareFurnizori(int? page)
        {
            var model = db.ComenziAprovizionari
                .Where(x => x.DeLaFurnizorId.HasValue)
                .Select(x => new ComandaAprovizionareReadAllViewModel
                {
                    NumarComanda = x.Id,
                    DataCreare = x.DataCreare,
                    Destinatar = x.CatreDepozitCentral.Denumire,
                    Expeditor = x.Furnizori.Denumire
                }).ToList();

            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        public ActionResult CreazaComandaAprovizionareFurnizor()
        {
            this.OnCreateFromFurnizor_InitView();
            return View("Creaza");
        }

        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        public PartialViewResult GetProduseFurnizor(int idFurnizor)
        {
            var viewModel = new ComandaAprivizionareCreate();
            //viewModel.Produse = new [] { new DetaliiProdusComanda() };
            ViewData.Add(AppConstants.CodProduseFurnizor, db.Produse.Where(x => x.IdFurnizor == idFurnizor).Select(x => x.Cod).ToArray());
            return PartialView("ComenziAprovizionare/ProduseFurnizor", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        public ActionResult CreazaComandaAprovizionareFurnizor(ComandaAprivizionareCreate viewModel)
        {
            var validationResult = new AprovizionareFurnizorValidationService().ValidateData(viewModel);
            if (validationResult.Any())
            {
                this.OnCreateFromFurnizor_InitView();
                ViewData.Add(AppConstants.Alerts.Error, validationResult);
                return View("Creaza", viewModel);
            }
            var model = new ComenziAprovizionari
            {
                DeLaFurnizorId = viewModel.IdExpeditor,
                CatreDepozitCentralId = this.db.Magazine.FirstOrDefault(x => x.EsteDepozitCentral).Id,
                DataCreare = DateTime.Now,
                StatusComanda = StatusComanda.Creata,
                IdUtilizator = this.db.Utilizatori.FirstOrDefault().Id
            };

            model.RandComenziAprovizionareProduse = new List<RandComenziAprovizionareProduse>();
            for (int i = 0; i < viewModel.Coduri.Length; ++i)
            {
                var cod = viewModel.Coduri[i];
                model.RandComenziAprovizionareProduse.Add(new RandComenziAprovizionareProduse
                {
                    IdProdus = db.Produse.FirstOrDefault(x => x.Cod == cod).Id,
                    CantitateCeruta = viewModel.Cantitati[i]
                });
            }
            db.ComenziAprovizionari.Add(model);
            db.SaveChanges();
            return RedirectToAction("ComenziAprovizionareFurnizori");
        }

        [HttpPost]
        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        public ActionResult AcceptaComandaAprovizionareFurnizor(ComandaAprivizionareReadOneViewModel model)
        {
            var dbComanda = this.db.ComenziAprovizionari
                .FirstOrDefault(x => x.Id == model.NumarComanda);
            db.ComenziAprovizionari.Attach(dbComanda);
            dbComanda.StatusComanda = StatusComanda.Finalizata;
            dbComanda.DataPrimire = DateTime.Now;
            var dbRandProduse = db.RandComenziAprovizionareProduse.Where(x => x.IdComanda == model.NumarComanda).ToArray();
            foreach (var rp in dbRandProduse)
            {
                db.RandComenziAprovizionareProduse.Attach(rp);
                rp.CantitatePrimita = model.Produse.FirstOrDefault(x => x.IdProdus == rp.IdProdus).CantitatePrimita;
            }
            var dbInventar = this.db.Inventar.Where(x => x.IdMagazin == model.CatreDepozitCentralId);
            foreach (var prod in model.Produse)
            {
                if (dbInventar.Any(x => x.IdProdus == prod.IdProdus))
                {
                    var dbInv = dbInventar.FirstOrDefault(x => x.IdProdus == prod.IdProdus);
                    this.db.Inventar.Attach(dbInv);
                    dbInv.CantitateDisponibila += prod.CantitatePrimita.Value;
                }
                else
                {
                    this.db.Inventar.Add(new Inventar
                    {
                        CantitateDisponibila = prod.CantitatePrimita.Value,
                        IdMagazin = model.CatreDepozitCentralId.Value,
                        IdProdus = prod.IdProdus
                    });
                }
            }
            db.SaveChanges();
            return RedirectToAction("ComenziAprovizionareFurnizori");
        }

        [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
        private void OnCreateFromFurnizor_InitView()
        {
            ViewData.Add(AppConstants.FurnizorOptions, this.db.Furnizori.ToSelectList(x => x.Id, x => x.Denumire));
        }

        #endregion

        #region De la Depozit Central
        [HttpGet]
        public ActionResult ComenziAprovizionareMagazine(int? page)
        {
            var model = db.ComenziAprovizionari
                .Where(x => x.DeLaDepozitCentralId.HasValue)
                .Select(x => new ComandaAprovizionareReadAllViewModel
                {
                    NumarComanda = x.Id,
                    DataCreare = x.DataCreare,
                    Destinatar = x.Magazine.Denumire,
                    Expeditor = x.DeLaDepozitCentral.Denumire
                }).ToList();

            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model);
        }


        [HttpGet]
        public ActionResult CreazaComandaAprovizionareMagazin()
        {
            var model = new ComandaAprivizionareCreate();
            model.IdExpeditor = this.db.Magazine.FirstOrDefault(x => x.EsteDepozitCentral).Id;
            return View("Creaza", model);
        }

        [HttpPost]
        public ActionResult CreazaComandaAprovizionareMagazin(ComandaAprivizionareCreate viewModel)
        {
            var validationResult = new AprovizionareMagazinValidationService().ValidateData(viewModel);
            if (validationResult.Any())
            {
                ViewData.Add(AppConstants.Alerts.Error, validationResult);
                return View("Creaza", viewModel);
            }
            var model = new ComenziAprovizionari
            {
                DeLaDepozitCentralId = viewModel.IdExpeditor,
                CatreMagazinId = this.db.Magazine.FirstOrDefault(x => !x.EsteDepozitCentral).Id,
                DataCreare = DateTime.Now,
                StatusComanda = StatusComanda.Creata,
                IdUtilizator = this.db.Utilizatori.FirstOrDefault().Id
            };

            model.RandComenziAprovizionareProduse = new List<RandComenziAprovizionareProduse>();
            for (int i = 0; i < viewModel.Coduri.Length; ++i)
            {
                var cod = viewModel.Coduri[i];
                model.RandComenziAprovizionareProduse.Add(new RandComenziAprovizionareProduse
                {
                    IdProdus = db.Produse.FirstOrDefault(x => x.Cod == cod).Id,
                    CantitateCeruta = viewModel.Cantitati[i]
                });
            }
            db.ComenziAprovizionari.Add(model);
            db.SaveChanges();
            return RedirectToAction("ComenziAprovizionareMagazine");
        }

        [HttpPost]
        public ActionResult AcceptaComandaAprovizionareMagazin(ComandaAprivizionareReadOneViewModel model)
        {
            var dbComanda = this.db.ComenziAprovizionari
                .FirstOrDefault(x => x.Id == model.NumarComanda);
            db.ComenziAprovizionari.Attach(dbComanda);
            dbComanda.StatusComanda = StatusComanda.Finalizata;
            dbComanda.DataPrimire = DateTime.Now;
            var dbRandProduse = db.RandComenziAprovizionareProduse.Where(x => x.IdComanda == model.NumarComanda).ToArray();
            foreach (var rp in dbRandProduse)
            {
                db.RandComenziAprovizionareProduse.Attach(rp);
                rp.CantitatePrimita = model.Produse.FirstOrDefault(x => x.IdProdus == rp.IdProdus).CantitatePrimita;
            }
            var dbInventar = this.db.Inventar.Where(x => x.IdMagazin == model.CatreMagazinId);
            var dbInventarDepozitCentral = this.db.Inventar.Where(x => x.IdMagazin == dbComanda.DeLaDepozitCentralId)
                .ToArray();
            foreach (var prod in model.Produse)
            {
                if (dbInventar.Any(x => x.IdProdus == prod.IdProdus))
                {
                    var dbInv = dbInventar.FirstOrDefault(x => x.IdProdus == prod.IdProdus);
                    this.db.Inventar.Attach(dbInv);
                    dbInv.CantitateDisponibila += prod.CantitatePrimita.Value;
                }
                else
                {
                    this.db.Inventar.Add(new Inventar
                    {
                        CantitateDisponibila = prod.CantitatePrimita.Value,
                        IdMagazin = model.CatreMagazinId.Value,
                        IdProdus = prod.IdProdus
                    });
                }

                var dbInvDC = dbInventarDepozitCentral.FirstOrDefault(x => x.IdProdus == prod.IdProdus);
                this.db.Inventar.Attach(dbInvDC);
                dbInvDC.CantitateDisponibila -= prod.CantitatePrimita.Value;
            }
            db.SaveChanges();
            return RedirectToAction("ComenziAprovizionareMagazine");
        }
        #endregion


        [HttpGet]
        public ActionResult PrimesteComanda(int idComanda)
        {
            var model = db.ComenziAprovizionari
                .Where(x => x.Id == idComanda)
                .ToArray()
                .Select(x => new ComandaAprivizionareReadOneViewModel
                {
                    NumarComanda = x.Id,
                    DeLaFurnizorId = x.DeLaFurnizorId,
                    DeLaDepozitCentralId = x.DeLaDepozitCentralId,
                    CatreDepozitCentralId = x.CatreDepozitCentralId,
                    CatreMagazinId = x.CatreMagazinId,
                    Status = x.StatusComanda,
                    Produse = x.RandComenziAprovizionareProduse.Select(y => new ComandaAprovizionareProdus
                    {
                        IdProdus = y.Produse.Id,
                        Cod = y.Produse.Cod,
                        Denumire = y.Produse.Denumire,
                        CantitateCeruta = y.CantitateCeruta,
                        CantitatePrimita = y.CantitatePrimita
                    }).ToArray()
                }).FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        public ActionResult VizualizeazaComanda(int id)
        {
            var model = db.ComenziAprovizionari
                .Include(x => x.CatreDepozitCentral)
                .Include(x => x.DeLaDepozitCentral)
                .Include(x => x.Furnizori)
                .Include(x => x.Magazine)
                .Include(x => x.RandComenziAprovizionareProduse)
                .Include(x => x.Utilizatori)
                .FirstOrDefault(x => x.Id == id);
            return View(model);
        }

    }
}