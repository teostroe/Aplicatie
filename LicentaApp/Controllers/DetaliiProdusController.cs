using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicentaApp;

namespace LicentaApp.Controllers
{
    public class DetaliiProdusController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: DetaliiProdus
        public ActionResult Index()
        {
            var detaliiProdus = db.DetaliiProdus.Include(d => d.Produse);
            return View(detaliiProdus.ToList());
        }

        // GET: DetaliiProdus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaliiProdus detaliiProdus = db.DetaliiProdus.Find(id);
            if (detaliiProdus == null)
            {
                return HttpNotFound();
            }
            return View(detaliiProdus);
        }

        // GET: DetaliiProdus/Create
        public ActionResult Create()
        {
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod");
            return View();
        }

        // POST: DetaliiProdus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denumire,Valoare,IdProdus")] DetaliiProdus detaliiProdus)
        {
            if (ModelState.IsValid)
            {
                db.DetaliiProdus.Add(detaliiProdus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", detaliiProdus.IdProdus);
            return View(detaliiProdus);
        }

        // GET: DetaliiProdus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaliiProdus detaliiProdus = db.DetaliiProdus.Find(id);
            if (detaliiProdus == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", detaliiProdus.IdProdus);
            return View(detaliiProdus);
        }

        // POST: DetaliiProdus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denumire,Valoare,IdProdus")] DetaliiProdus detaliiProdus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detaliiProdus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", detaliiProdus.IdProdus);
            return View(detaliiProdus);
        }

        // GET: DetaliiProdus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetaliiProdus detaliiProdus = db.DetaliiProdus.Find(id);
            if (detaliiProdus == null)
            {
                return HttpNotFound();
            }
            return View(detaliiProdus);
        }

        // POST: DetaliiProdus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetaliiProdus detaliiProdus = db.DetaliiProdus.Find(id);
            db.DetaliiProdus.Remove(detaliiProdus);
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
