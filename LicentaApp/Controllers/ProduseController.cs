using System;
using System.Collections.Generic;
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

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
    public class ProduseController : BaseAppController
    {
        // GET: Produse
        public ActionResult Index(int? page)
        {
            var produse = db.Produse.Include(p => p.Furnizori)
                .Include(x => x.Preturi)
                .OrderBy(x => x.TipProdus)
                .ThenBy(x => x.Denumire);
            ViewData.InitializePagination(page, produse.Count(), this.ControllerContext);
            return View(produse.ToPagedList(page));
        }

        // GET: Produse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produse produse = db.Produse.Find(id);
            if (produse == null)
            {
                return HttpNotFound();
            }
            return View(produse);
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
            var dbResult = this.SaveChages();
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
            dbProduse.Discount = model.Produse.Discount;
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

            var result = this.SaveChages();
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
            Produse produse = db.Produse.Find(id);
            db.Produse.Remove(produse);
            db.SaveChanges();
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
