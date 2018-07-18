using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;
using LicentaApp;
using LicentaApp.Controllers.Base;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Metadata;
using LicentaApp.Domain.ValueObjects;
using LicentaApp.ViewModels;
using LicentaApp.ViewModels.Produse;
using LicentaApp.ViewModels.Rapoarte.Search;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ProduseController : BaseAppController
    {
        // GET: Produse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaLentile(int? page)
        {
            var model = this.db.LentileView.AsQueryable();
            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model.ToPagedList(page));
        }

        public ActionResult FilterListaLentile(LentileFilter filter)
        {
            var model = this.db.LentileView.AsQueryable();

            if (!filter.Cod.IsNullOrEmpty())
            {
                model = model.Where(x => x.Cod.Contains(filter.Cod));
            }
            if (!filter.Denumire.IsNullOrEmpty())
            {
                model = model.Where(x => x.Denumire.Contains(filter.Denumire));
            }
            if (filter.Discount.HasValue)
            {
                model = model.Where(x => x.Discount == filter.Discount);
            }
            if (!filter.TipLentila.IsNullOrEmpty())
            {
                model = model.Where(x => x.TipLentila.Contains(filter.TipLentila));
            }
            if (!filter.IndiceRefractie.IsNullOrEmpty())
            {
                model = model.Where(x => x.IndiceRefractie == filter.IndiceRefractie);
            }
            if (filter.Pret.HasValue)
            {
                model = model.Where(x => x.Pret == filter.Pret.Value);
            }
            
            ViewData.InitializePagination(filter.Page, model.Count(), this.ControllerContext);
            return PartialView("Produse/ListaLentileData", model.ToPagedList(filter.Page));
        }

        public ActionResult ListaRame(int? page)
        {
            var model = this.db.RameView.AsQueryable();
            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model.ToPagedList(page));
        }

        public ActionResult FiltreazaListaRame(RameFilter filter)
        {
            var model = this.db.RameView.AsQueryable();
            if (!filter.Cod.IsNullOrEmpty())
            {
                model = model.Where(x => x.Cod.Contains(filter.Cod));
            }
            if (!filter.Denumire.IsNullOrEmpty())
            {
                model = model.Where(x => x.Denumire.Contains(filter.Denumire));
            }
            if (filter.Discount.HasValue)
            {
                model = model.Where(x => x.Discount == filter.Discount);
            }
            if (filter.Pret.HasValue)
            {
                model = model.Where(x => x.Valoare == filter.Pret.Value);
            }

            ViewData.InitializePagination(filter.Page, model.Count(), this.ControllerContext);
            return PartialView("Produse/ListaRameData", model.ToPagedList(filter.Page));
        }

        public ActionResult ListaOchelariDeSoare(int? page)
        {
            var model = this.db.OchelariDeSoareView.AsQueryable();
            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model.ToPagedList(page));
        }

        public ActionResult FiltreazaListaOchelariSoare(OchelariSoareFilter filter)
        {
            var model = this.db.OchelariDeSoareView.AsQueryable();
            if (!filter.Cod.IsNullOrEmpty())
            {
                model = model.Where(x => x.Cod.Contains(filter.Cod));
            }
            if (!filter.Denumire.IsNullOrEmpty())
            {
                model = model.Where(x => x.Denumire.Contains(filter.Denumire));
            }
            if (filter.Discount.HasValue)
            {
                model = model.Where(x => x.Discount == filter.Discount);
            }
            if (!filter.EstePolarizat.IsNullOrEmpty())
            {
                model = model.Where(x => x.EstePolarizat == filter.EstePolarizat);
            }
            if (filter.Pret.HasValue)
            {
                model = model.Where(x => x.Pret == filter.Pret.Value);
            }

            ViewData.InitializePagination(filter.Page, model.Count(), this.ControllerContext);
            return PartialView("Produse/ListaOchelariSoareData", model.ToPagedList(filter.Page));
        }

        // GET: Produse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = User as AppPrincipal;

            var model = db.Produse.Where(x => x.Id == id)
                .Select(x => new ProdusReadOneViewModel
                {
                    Produs = x,
                    DetaliiProdus = x.DetaliiProdus,
                    InventarMagazin = x.Inventar.FirstOrDefault(y => y.IdMagazin == user.IdMagazin).CantitateDisponibila,
                    InventarDepozitCentral = x.Inventar.FirstOrDefault(y => y.Magazine.EsteDepozitCentral).CantitateDisponibila
                }).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult GetProductProperties(TipProdus tipProdus)
        {
            var model = new ProduseViewModel
            {
                ProductMetadata = ProductMetadata.GetAllForProductType(tipProdus)
            };
            return PartialView("Produse/ProductProperties", model);
        }

        // GET: Produse/Create
        public ActionResult Create()
        {
            this.InitViewBag();
            return View();
        }

        // POST: Produse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProduseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.InitViewBag();
                this.ReInitViewModel(ref model);
                return View(model);
            }

            model.Produse.DetaliiProdus = new List<DetaliiProdus>();
            model.Produse.Preturi = new List<Preturi>();

            model.Produse.Discount = model.Produse.Discount ?? 0;

            model.Produse.Preturi.Add(new Preturi
            {
                DataActualizare = DateTime.UtcNow,
                EsteUtilizatAcum = true,
                Valoare = model.Pret
            });
            foreach (var prop in model.ProductProperties)
            {
                model.Produse.DetaliiProdus.Add(new DetaliiProdus
                {
                    Denumire = prop.Key,
                    Valoare = prop.Value
                });
            }
            db.Produse.Add(model.Produse);
            var dbResult = this.SaveChanges();
            if (dbResult != DbSaveResult.Success)
            {
                this.InitViewBag();
                this.ReInitViewModel(ref model);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Produse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produse produse = db.Produse.Include(x => x.Preturi).Include(x => x.DetaliiProdus).SingleOrDefault(x => x.Id == id);
            if (produse == null)
            {
                return HttpNotFound();
            }

            var model = new ProduseViewModel
            {
                Produse = produse,
                ProductMetadata = ProductMetadata.GetAllForProductType(produse.TipProdus),
                Pret = produse.Preturi.SingleOrDefault(x => x.EsteUtilizatAcum).Valoare,
                ProductProperties = db.DetaliiProdus.Where(x => x.IdProdus == produse.Id).ToDictionary(x => x.Denumire, x => x.Valoare)
            };

            this.InitViewBag();
            return View(model);
        }

        // POST: Produse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProduseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.InitViewBag();
                this.ReInitViewModel(ref model);
                return View(model);
            }

            var dbProduse = this.db.Produse.SingleOrDefault(x => x.Id == model.Produse.Id);
            db.Produse.Attach(dbProduse);
            dbProduse.TipProdus = model.Produse.TipProdus;
            dbProduse.Cod = model.Produse.Cod;
            dbProduse.Discount = model.Produse.Discount ?? 0;
            dbProduse.IdFurnizor = model.Produse.IdFurnizor;
            var detaliiprodus = db.DetaliiProdus.Where(x => x.IdProdus == model.Produse.Id).ToArray();
            foreach (var dp in detaliiprodus)
            {
                db.DetaliiProdus.Attach(dp);
                dp.Valoare = model.ProductProperties[dp.Denumire];
            }
            var pretUtilizat = db.Preturi.SingleOrDefault(x => x.IdProdus == model.Produse.Id && x.EsteUtilizatAcum);
            if (pretUtilizat != null)
            {
                db.Preturi.Attach(pretUtilizat);
                pretUtilizat.EsteUtilizatAcum = false;
            }
            db.Preturi.Add(new Preturi
            {
                DataActualizare = DateTime.UtcNow,
                EsteUtilizatAcum = true,
                Valoare = model.Pret,
                IdProdus = model.Produse.Id
            });

            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                this.InitViewBag();
                this.ReInitViewModel(ref model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: Produse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (this.db.RandComenziProduse.Any(x => x.IdProdus == id) ||
                this.db.RandComenziAprovizionareProduse.Any(x => x.IdProdus == id))
            {
                TempData.Add(AppConstants.Alerts.Error, new[] { new ValidationResult("Produsul nu poate fi sters deoarece a fost deja folosit") });
                return RedirectToAction("Details", new { id = id });
            }

            Produse produse = db.Produse.Find(id);
            db.Produse.Remove(produse);
            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Index");
        }

        //// POST: Produse/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Produse produse = db.Produse.Find(id);
        //    db.Produse.Remove(produse);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitViewBag()
        {
            ViewBag.FurnizoriSelectList = db.Furnizori.ToSelectList(x => x.Id, x => x.Denumire);
        }

        private void ReInitViewModel(ref ProduseViewModel model)
        {
            model.ProductMetadata = ProductMetadata.GetAllForProductType(model.Produse.TipProdus);
        }

    }
}

