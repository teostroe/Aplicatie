using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LicentaApp;
using LicentaApp.Controllers.Base;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Metadata;
using LicentaApp.Domain.Services;
using LicentaApp.Domain.Services.Helpers;
using LicentaApp.Domain.Services.ValidationServices.Implementations;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels;
using LicentaApp.ViewModels.Comanda;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ComenziController : BaseAppController
    {
        private Dictionary<TipLentila, decimal[]> _indiciRefractie = new Dictionary<TipLentila, decimal[]>()
        {
            {TipLentila.MonofocalaUniforma, new  [] {1.5m, 1.6m, 1.67m, 1.74m}},
            {TipLentila.Bifocala, new [] {1.5m, 1.6m} },
            {TipLentila.Minerala, new[] {1.52m, 167m}},
            {TipLentila.Progresiva, new  [] {1.5m, 1.6m, 1.67m, 1.74m}  }
        };
        // GET: Comenzi
        public ActionResult Index(int? page)
        {
            var comenzi = db.Comenzi
                .Include(c => c.Clienti)
                .Include(c => c.Utilizatori)
                .Include(c => c.ViziteMedicale)
                .ToList()
                .Select(x => new ComandaReadAllViewModel
                {
                    NumarComanda = x.Id,
                    Data = x.Data,
                    NumeClient = $"{x.Clienti.Nume} {x.Clienti.Prenume}",
                    NumeAngajat = $"{x.Utilizatori.Nume} {x.Utilizatori.Prenume}",
                    //Discount = x.Discount,
                    //Total = ProductsDataService.GetTotal(x.Id, db)
                })
                .OrderByDescending(x => x.Data);

            ViewData.InitializePagination(page, comenzi.Count(), this.ControllerContext);

            return View(comenzi.ToPagedList(page));
        }

        // GET: Comenzi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!db.Comenzi.Any(x => x.Id == id))
            {
                return HttpNotFound();
            }

            var comanda = db.Comenzi
                .Where(x => x.Id == id)
                .Include(x => x.Clienti)
                .Include(x => x.ViziteMedicale)
                .Include(x => x.RandComenziProduse)
                .Single();
            var model = new ComandaReadOneViewModel
            {
                NumarComanda = comanda.Id,
                Client = comanda.Clienti,
                VizitaMedicala = comanda.ViziteMedicale,
                Discount = comanda.Discount,
                StatusComanda = comanda.StatusComanda,
                Data = comanda.Data,
                Produse = comanda.RandComenziProduse.Select(x => new ComandaProdusViewModel
                {
                    Cod = x.Produse.Cod,
                    TipProdus = x.Produse.TipProdus,
                    Cantitate = x.Cantitate,
                    Denumire = x.Produse.Denumire,
                    Pret = ProductsDataService.GetPret(x.IdProdus, comanda.Data, db),
                    Discount = x.Produse.Discount
                })
            };
            var totalFaraDiscount = model.Produse.Sum(x => x.Pret - x.Pret * x.Discount);
            model.Total = totalFaraDiscount - totalFaraDiscount * model.Discount;
            return View(model);
        }

        public ActionResult Create()
        {
            if (TempData.ContainsKey(AppConstants.TempComandaViewModel))
            {
                var viewModel = (ComandaViewModel)TempData[AppConstants.TempComandaViewModel];
                this.OnCreate_ViewDataInit(viewModel);
                return View(viewModel);
            }

            this.OnCreate_ViewDataInit();
            return View();
        }

        public PartialViewResult GetTipLentilaOptions(string[] ids)
        {
            var model = new ComandaViewModel();
            if (ids != null)
            {
                var doarLentilaProgresiva = ids.Any(x => x.Contains("Aproape")) && ids.Any(x => x.Contains("Distanta"));
                this.AddTipLentilaOptionsToViewData(doarLentilaProgresiva);
            }

            return PartialView("Comenzi/TipLentilaOptions", model);
        }

        public PartialViewResult GetIndiceRefractieOptions(TipLentila tipLentila)
        {
            var model = new ComandaViewModel();
            this.AddIndiceRefractieOptionsToViewData(tipLentila);
            return PartialView("Comenzi/IndiceRefractieOptions", model);
        }

        public PartialViewResult GetProducatorOptions(TipLentila tipLentila, decimal indiceRefractie)
        {
            var model = new ComandaViewModel();
            this.AddProducatorOptionsToViewData(tipLentila, indiceRefractie);
            return PartialView("Comenzi/ProducatorOptions", model);
        }

        public PartialViewResult GetLentilaOptions(TipLentila tipLentila, decimal indiceRefractie, int idProducator)
        {
            var model = new ComandaViewModel();
            this.AddLentilaOptionsToViewData(tipLentila, indiceRefractie, idProducator);
            return PartialView("Comenzi/LentilaOptions", model);
        }

        public PartialViewResult GetLentileExtraOptions()
        {
            var model = new ComandaViewModel();

            this.AddLentileExtraOptionsToViewData();
            return PartialView("Comenzi/LentileExtraOptions", model);
        }

        public PartialViewResult GetClient(string numarTelefon)
        {
            var client = db.Clienti.FirstOrDefault(x => x.NumarTelefon == numarTelefon);
            if (client == null)
            {
                return null;
            }
            return PartialView("Comenzi/EditClient", new ComandaViewModel { Client = client });
        }

        public PartialViewResult GetComandaDupaTip(TipComanda tipComanda)
        {
            var model = new ComandaViewModel();
            switch (tipComanda)
            {
                case TipComanda.ComandaCuPrelucrare:
                    this.AddVizitaMedicalaOptionsToViewData();
                    return PartialView("Comenzi/ComandaCuPrelucrare", model);

                case TipComanda.ComandaSimpla:
                    return PartialView("Comenzi/ComandaSimpla", model);
                default:
                    throw new NotImplementedException();
            }
        }

        [HttpGet]
        public ActionResult VerificareComanda(ComandaViewModel viewModel)
        {
            var validationResult = new ComandaValidationService().ValidateData(viewModel);
            if (validationResult.Any())
            {
                this.OnCreate_ViewDataInit(viewModel);
                ViewData.Add(AppConstants.Alerts.Error, validationResult);
                return View("Create", viewModel);
            }
            if (viewModel.Client.DataInregistrare == default(DateTime))
            {
                viewModel.Client.DataInregistrare = DateTime.Now;
            }

            if (viewModel.TipComanda == TipComanda.ComandaCuPrelucrare)
            {
                viewModel.Manopera = 50m;

            }
            TempData[AppConstants.TempComandaViewModel] = viewModel;
            var dictionary = new Dictionary<string, ProdusVerificareViewModel>();
            if (viewModel.Lentila != null)
            {
                var lentila = this.GetProdusVerificare(viewModel.Lentila.CodProdus);
                if ((viewModel.VizitaMedicala.SferaAproapeStang.HasValue ||
                     viewModel.VizitaMedicala.SferaDistantaStang.HasValue) &&
                    (viewModel.VizitaMedicala.SferaAproapeDrept.HasValue ||
                     viewModel.VizitaMedicala.SferaDistantaDrept.HasValue))
                {
                    lentila.Cantitate = 2;
                }
                else
                {
                    lentila.Cantitate = 1;
                }
                dictionary.Add(viewModel.Lentila.CodProdus, lentila);
            }
            if (!viewModel.CodProdusRO.IsNullOrEmpty())
            {
                var produs = this.GetProdusVerificare(viewModel.CodProdusRO);
                produs.Cantitate = 1;
                dictionary.Add(viewModel.CodProdusRO, produs);
            }

            ViewData.Add(AppConstants.VerificareProduse, dictionary);
            return View("VerificareComanda", viewModel);
        }

        private ProdusVerificareViewModel GetProdusVerificare(string codProdus)
        {
            return this.db.Produse.Where(x => x.Cod == codProdus)
                .Select(x => new ProdusVerificareViewModel
                {
                    DenumireProduse = x.Denumire,
                    Discount = x.Discount,
                    Pret = x.Preturi.FirstOrDefault(y => y.EsteUtilizatAcum).Valoare,
                    TipProdus = x.TipProdus
                }).FirstOrDefault();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComandaViewModel viewModel)
        {
            if (TempData.ContainsKey(AppConstants.TempComandaViewModel))
            {
                viewModel = (ComandaViewModel)TempData[AppConstants.TempComandaViewModel];
            }

            //comanda lentile
            var comanda = new Comenzi();
            comanda.RandComenziProduse = new List<RandComenziProduse>();
            comanda.Data = DateTime.Now;
            comanda.Discount = viewModel.Discount;
            comanda.StatusComanda = StatusComanda.Creata;
            //to be updated
            comanda.IdUtilizator = this.db.Utilizatori.FirstOrDefault().Id;
            #region Client
            if (db.Clienti.Any(x => x.Id == viewModel.Client.Id))
            {
                var dbClient = db.Clienti.SingleOrDefault(x => x.Id == viewModel.Client.Id);
                db.Clienti.Attach(dbClient);
                dbClient.Email = viewModel.Client.Email;
                dbClient.NumarTelefon = viewModel.Client.NumarTelefon;
                dbClient.Profesie = viewModel.Client.Profesie;
                comanda.IdClient = viewModel.Client.Id;
            }
            else
            {
                viewModel.Client.DataInregistrare = DateTime.Now;
                comanda.Clienti = viewModel.Client;
            }
            #endregion

            #region Lentila & Vizita Medicala
            if (viewModel.Lentila != null)
            {
                comanda.ViziteMedicale = viewModel.VizitaMedicala;
                var dbProdus = db.Produse.Single(x => x.Cod == viewModel.Lentila.CodProdus);
                comanda.RandComenziProduse.Add(new RandComenziProduse
                {
                    Cantitate = 2,
                    IdProdus = dbProdus.Id,
                    Discount = dbProdus.Discount,
                    TipTratement = viewModel.Lentila.Tratement,
                    TipCuloare = viewModel.Lentila.Culoare
                });
            }
            #endregion

            #region Rame||Ochelari de Soare

            if (!viewModel.CodProdusRO.IsNullOrEmpty())
            {
                comanda.RandComenziProduse.Add(new RandComenziProduse
                {
                    Cantitate = 1,
                    IdProdus = db.Produse.Single(x => x.Cod == viewModel.CodProdusRO).Id
                });
            }

            #endregion

            db.Comenzi.Add(comanda);
            db.SaveChanges();
            if (TempData.ContainsKey(AppConstants.TempComandaViewModel))
            {
                TempData.Remove(AppConstants.TempComandaViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ModificaStatus(int idComanda)
        {
            var dbComanda = this.db.Comenzi.FirstOrDefault(x => x.Id == idComanda);
            db.Comenzi.Attach(dbComanda);
            dbComanda.StatusComanda = dbComanda.StatusComanda.GetNextState();
            db.SaveChanges();
            return RedirectToAction("Details", new { id = idComanda });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnCreate_ViewDataInit(ComandaViewModel viewModel = null)
        {
            ViewData.Add(AppConstants.TelefonClienti, db.Clienti.Select(x => x.NumarTelefon).ToArray());
            ViewData.Add(AppConstants.CodRame, db.Produse.Where(x => x.TipProdus == TipProdus.Rame).Select(x => x.Cod).ToArray());
            ViewData.Add(AppConstants.CodRameSiOchelariSoare, db.Produse.Where(x => new[] { TipProdus.Rame, TipProdus.OchelariSoare, }.Contains(x.TipProdus)).Select(x => x.Cod).ToArray());

            if (viewModel == null) return;

            if (viewModel.TipComanda == TipComanda.ComandaCuPrelucrare)
            {
                this.AddVizitaMedicalaOptionsToViewData();
            }

            if (viewModel.VizitaMedicala == null) return;

            bool doarLentilaProgresiva =
                (viewModel.VizitaMedicala.SferaAproapeDrept.HasValue ||
                 viewModel.VizitaMedicala.SferaDistantaDrept.HasValue ||
                 viewModel.VizitaMedicala.PrismaDrept.HasValue) &&
                (viewModel.VizitaMedicala.SferaAproapeStang.HasValue ||
                 viewModel.VizitaMedicala.SferaDistantaStang.HasValue ||
                 viewModel.VizitaMedicala.PrismaStang.HasValue);
            AddTipLentilaOptionsToViewData(doarLentilaProgresiva);

            if (viewModel.Lentila == null) return;

            if (viewModel.Lentila.TipLentila.HasValue)
            {
                this.AddIndiceRefractieOptionsToViewData(viewModel.Lentila.TipLentila.Value);
            }

            if (viewModel.Lentila.IndiceRefractie.HasValue)
            {
                this.AddProducatorOptionsToViewData(viewModel.Lentila.TipLentila.Value, viewModel.Lentila.IndiceRefractie.Value);
            }

            if (viewModel.Lentila.ProducatorId.HasValue)
            {
                this.AddLentilaOptionsToViewData(viewModel.Lentila.TipLentila.Value,
                    viewModel.Lentila.IndiceRefractie.Value, viewModel.Lentila.ProducatorId.Value);
            }
            if (!viewModel.Lentila.CodProdus.IsNullOrEmpty())
            {
                this.AddLentileExtraOptionsToViewData();
            }
        }

        private void AddTipLentilaOptionsToViewData(bool doarLentilaProgresiva)
        {
            if (doarLentilaProgresiva)
            {
                ViewData.AddOrUpdate(AppConstants.TipLentilaOptions,
                    typeof(TipLentila).ToSelectList(TipLentila.Bifocala, TipLentila.MonofocalaUniforma, TipLentila.Minerala));
            }
            else
            {
                ViewData.AddOrUpdate(AppConstants.TipLentilaOptions,
                    typeof(TipLentila).ToSelectList());
            }
        }

        private void AddIndiceRefractieOptionsToViewData(TipLentila tipLentila)
        {
            ViewData.AddOrUpdate(AppConstants.IndiceReractieOptions, new SelectList(this._indiciRefractie[tipLentila]));

        }

        private void AddProducatorOptionsToViewData(TipLentila tipLentila, decimal indiceRefractie)
        {
            var data = new Dictionary<string, object>()
            {
                {ProductProperties.TipLentila, tipLentila},
                {ProductProperties.IndiceRefractie, indiceRefractie}
            };
            var serializedValues = SerializationService.SerializeProductData(data);

            var produseSelectlist = ProductsDataService.GetFurnizori(serializedValues, this.db).ToSelectList(x => x.Id, x => x.Denumire);
            ViewData.AddOrUpdate(AppConstants.ProducatorOptions, produseSelectlist);
        }

        private void AddLentilaOptionsToViewData(TipLentila tipLentila, decimal indiceRefractie, int idProducator)
        {
            var data = new Dictionary<string, object>()
            {
                {ProductProperties.TipLentila, tipLentila},
                {ProductProperties.IndiceRefractie, indiceRefractie}
            };
            var serializedValues = SerializationService.SerializeProductData(data);
            var selectList = ProductsDataService.GetProducts(serializedValues, this.db).Where(x => x.IdFurnizor == idProducator).ToSelectList(x => x.Cod, x => x.Denumire);
            ViewData.AddOrUpdate(AppConstants.LentilaOptions, selectList);
        }

        private void AddLentileExtraOptionsToViewData()
        {
            var tratamentSelectList = typeof(TipTratament).ToSelectList();
            var culoareSelectList = typeof(TipCuloare).ToSelectList();
            ViewData.AddOrUpdate(AppConstants.TratamentOptions, tratamentSelectList);
            ViewData.AddOrUpdate(AppConstants.CuloareOptions, culoareSelectList);
        }

        private void AddVizitaMedicalaOptionsToViewData()
        {
            ViewData.Add(AppConstants.SferaDistantaOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.SferaAproapeOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.CilindruOptions, OptionsGenerationService.GenerateSelectListForValues(0.00, 16.00, 0.25));
            ViewData.Add(AppConstants.AxOptions, OptionsGenerationService.GenerateSelectListForValues(0, 360, 1));
            ViewData.Add(AppConstants.PrismaOptions, OptionsGenerationService.GenerateSelectListForValues(0.0, 10.0, 0.5));
        }
    }
}







