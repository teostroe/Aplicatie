using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain;
using LicentaApp.Domain.Services.ValidationServices.Implementations;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels.ComandaAprovizionare;

namespace LicentaApp.Controllers
{
    public class ComenziAprovizionareController : Controller
    {

        private LicentaDbContext db = new LicentaDbContext();

        // GET: ComenziAprovizionare
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

        public ActionResult ComenziAprovizionareMagazine(int? page)
        {
            var model = db.ComenziAprovizionari
                .Where(x => x.DeLaDepozitCentralId.HasValue)
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
        public ActionResult CreazaComandaAprovizionareFurnizor()
        {
            this.OnCreate_InitView();
            return View("Creaza");  
        }

        public PartialViewResult GetProduseFuurnizor(int idFurnizor)
        {
            var viewModel = new ComandaAprivizionareCreate();
            //viewModel.Produse = new [] { new DetaliiProdusComanda() };
            ViewData.Add(AppConstants.CodProduseFurnizor, db.Produse.Where(x => x.IdFurnizor == idFurnizor).Select(x => x.Cod).ToArray());
            return PartialView("ComenziAprovizionare/ProduseFurnizor", viewModel);
        }

        [HttpPost]
        public ActionResult CreazaComandaAprovizionareFurnizor(ComandaAprivizionareCreate viewModel)
        {
            var validationResult = new AprovizionareFurnizorValidationService().ValidateData(viewModel);
            if (validationResult.Any())
            {
                this.OnCreate_InitView();
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

        [HttpGet]
        public ActionResult AcceptaComandaAprovizionareFurnizor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcceptaComandaAprovizionareFurnizor(string obj)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreazaComandaAprovizionareMagazin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreazaComandaAprovizionareMagazin(Dictionary<string, string> prods)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AcceptaComandaAprovizionareMagazin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcceptaComandaAprovizionareMagazin(string obj)
        {
            return View();
        }

        [HttpGet]
        public ActionResult VizualizeazaComanda(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult TrimiteComanda(int id)
        {
            return View();
        }

        private void OnCreate_InitView()
        {
            ViewData.Add(AppConstants.FurnizorOptions, this.db.Furnizori.ToSelectList(x => x.Id, x => x.Denumire));
        }
    }
}