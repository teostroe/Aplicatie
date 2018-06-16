using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicentaApp;
using LicentaApp.Domain;
using LicentaApp.Domain.Metadata;
using LicentaApp.Domain.Services;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels;
using Microsoft.Ajax.Utilities;

namespace LicentaApp.Controllers
{
    public class ComenziController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();
        private Dictionary<TipLentila, decimal[]> _indiciRefractie = new Dictionary<TipLentila, decimal[]>()
        {
            {TipLentila.MonofocalaUniforma, new  [] {1.5m, 1.6m, 1.67m, 1.74m}},
            {TipLentila.Bifocala, new [] {1.5m, 1.6m} },
            {TipLentila.Minerala, new[] {1.52m, 167m}},
            {TipLentila.Progresiva, new  [] {1.5m, 1.6m, 1.67m, 1.74m}  }
        };
        // GET: Comenzi
        public ActionResult Index()
        {
            var comenzi = db.Comenzi.Include(c => c.Clienti).Include(c => c.Utilizatori).Include(c => c.ViziteMedicale);
            return View(comenzi.ToList());
        }

        // GET: Comenzi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            return View(comenzi);
        }

        // GET: Comenzi/Create
        public ActionResult Create()
        {
            ViewData.Add(AppConstants.SferaDistantaOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.SferaAproapeOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.CilindruOptions, OptionsGenerationService.GenerateSelectListForValues(0.00, 16.00, 0.25));
            ViewData.Add(AppConstants.AxOptions, OptionsGenerationService.GenerateSelectListForValues(0, 360, 1));
            ViewData.Add(AppConstants.PrismaOptions, OptionsGenerationService.GenerateSelectListForValues(0.0, 10.0, 0.5));
            ViewData.Add(AppConstants.TelefonClienti, db.Clienti.Select(x => x.NumarTelefon).ToArray());
            ViewData.Add(AppConstants.CodRame, db.Produse.Where(x => x.TipProdus == TipProdus.Rame).Select(x => x.Cod).ToArray());
            ViewData.Add(AppConstants.CodRameSiOchelariSoare, db.Produse.Where(x => new[] { TipProdus.Rame, TipProdus.OchelariSoare, }.Contains(x.TipProdus)).Select(x => x.Cod).ToArray());
            return View();
        }

        public PartialViewResult GetTipLentilaOptions(bool doarLentilaProgresiva)
        {
            var model = new ComandaViewModel();
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
            return PartialView("Comenzi/TipLentilaOptions", model);
        }

        public PartialViewResult GetIndiceRefractieOptions(TipLentila tipLentila)
        {
            var model = new ComandaViewModel();
            ViewData.AddOrUpdate(AppConstants.IndiceReractieOptions, new SelectList(this._indiciRefractie[tipLentila]));
            return PartialView("Comenzi/IndiceRefractieOptions", model);
        }

        public PartialViewResult GetProducatorOptions(TipLentila tipLentila, decimal indiceRefractie)
        {
            var model = new ComandaViewModel();
            var data = new Dictionary<string, object>()
            {
                {ProductProperties.TipLentila, tipLentila},
                {ProductProperties.IndiceRefractie, indiceRefractie}
            };
            var serializedValues = SerializationService.SerializeProductData(data);

            var produseSelectlist = ProductsDataService.GetFurnizori(serializedValues, this.db).ToSelectList(x => x.Id, x => x.Denumire);
            ViewData.AddOrUpdate(AppConstants.ProducatorOptions, produseSelectlist);
            return PartialView("Comenzi/ProducatorOptions", model);
        }

        public PartialViewResult GetLentilaOptions(TipLentila tipLentila, decimal indiceRefractie, int idProducator)
        {
            var model = new ComandaViewModel();
            var data = new Dictionary<string, object>()
            {
                {ProductProperties.TipLentila, tipLentila},
                {ProductProperties.IndiceRefractie, indiceRefractie}
            };
            var serializedValues = SerializationService.SerializeProductData(data);
            var selectList = ProductsDataService.GetProducts(serializedValues, this.db).Where(x => x.IdFurnizor == idProducator).ToSelectList(x => x.Cod, x => x.Denumire);
            ViewData.AddOrUpdate(AppConstants.LentilaOptions, selectList);
            return PartialView("Comenzi/LentilaOptions", model);
        }

        public PartialViewResult GetTratamentOptions(int idProdus)
        {
            var model = new ComandaViewModel();

            var tratamentSelectList = typeof(TipTratament).ToSelectList();
            var culoareSelectList = typeof(TipCuloare).ToSelectList();
            ViewData.AddOrUpdate(AppConstants.TratamentOptions, tratamentSelectList);
            ViewData.AddOrUpdate(AppConstants.CuloareOptions, culoareSelectList);
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
            ViewData.Add(AppConstants.SferaDistantaOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.SferaAproapeOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.CilindruOptions, OptionsGenerationService.GenerateSelectListForValues(0.00, 16.00, 0.25));
            ViewData.Add(AppConstants.AxOptions, OptionsGenerationService.GenerateSelectListForValues(0, 360, 1));
            ViewData.Add(AppConstants.PrismaOptions, OptionsGenerationService.GenerateSelectListForValues(0.0, 10.0, 0.5));
            switch (tipComanda)
            {
                case TipComanda.ComandaCuPrelucrare:
                    return PartialView("Comenzi/ComandaCuPrelucrare", model);

                case TipComanda.ComandaSimpla:
                    return PartialView("Comenzi/ComandaSimpla", model);
                default:
                    throw new NotImplementedException();
            }
        }

        //public PartialViewResult GetCuloareOptions(int idProdus)
        //{

        //}

        [HttpPost]
        public PartialViewResult DisplayVizitaMedicala(ComandaViewModel viewModel)
        {
            return PartialView("Comenzi/Displays/VizitaMedicala", viewModel.VizitaMedicala);
        }

        [HttpGet]
        public ActionResult Create_Step1()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Create_Step1(Clienti client)
        {
            if (TempData.ContainsKey("Client"))
            {
                TempData["Client"] = client;
            }
            else
            {
                TempData.Add("Client", client);
            }
            ViewData.Add(AppConstants.SferaDistantaOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.SferaAproapeOptions, OptionsGenerationService.GenerateSelectListForValues(-19.00, 16.00, 0.25));
            ViewData.Add(AppConstants.CilindruOptions, OptionsGenerationService.GenerateSelectListForValues(0.00, 16.00, 0.25));
            ViewData.Add(AppConstants.AxOptions, OptionsGenerationService.GenerateSelectListForValues(0, 360, 1));
            ViewData.Add(AppConstants.PrismaOptions, OptionsGenerationService.GenerateSelectListForValues(0.0, 10.0, 0.5));

            return PartialView("Create_Step2");
        }

        [HttpPost]
        public PartialViewResult Create_Step2(ViziteMedicale vizitaMedicala)
        {
            TempData.Add("VizitaMedicala", vizitaMedicala);
            return PartialView("Create_Step3");
        }



        // POST: Comenzi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComandaViewModel viewModel)
        {
            //comanda lentile
            var comanda = new Comenzi();
            comanda.RandComenziProduse = new List<RandComenziProduse>();
            comanda.Data = DateTime.Now;
            comanda.Discount = viewModel.Discount;
            //to be updated
            comanda.IdUtilizator = 2;
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
                comanda.RandComenziProduse.Add(new RandComenziProduse
                {
                    Cantitate = 2,
                    IdProdus = db.Produse.Single(x => x.Cod == viewModel.Lentila.CodProdus).Id,
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
            return RedirectToAction("Index");
        }

        // GET: Comenzi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume", comenzi.IdClient);
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username", comenzi.IdUtilizator);
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala", comenzi.Id);
            return View(comenzi);
        }

        // POST: Comenzi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Discount,IdUtilizator,IdClient")] Comenzi comenzi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comenzi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume", comenzi.IdClient);
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username", comenzi.IdUtilizator);
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala", comenzi.Id);
            return View(comenzi);
        }

        // GET: Comenzi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            return View(comenzi);
        }

        // POST: Comenzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comenzi comenzi = db.Comenzi.Find(id);
            db.Comenzi.Remove(comenzi);
            db.SaveChanges();
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
