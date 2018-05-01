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
    public class PreturiController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Preturi
        public ActionResult Index()
        {
            var preturi = db.Preturi.Include(p => p.Produse);
            return View(preturi.ToList());
        }

        // GET: Preturi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preturi preturi = db.Preturi.Find(id);
            if (preturi == null)
            {
                return HttpNotFound();
            }
            return View(preturi);
        }

        // GET: Preturi/Create
        public ActionResult Create()
        {
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod");
            return View();
        }

        // POST: Preturi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valoare,DataActualizare,EsteUtilizatAcum,IdProdus")] Preturi preturi)
        {
            if (ModelState.IsValid)
            {
                db.Preturi.Add(preturi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", preturi.IdProdus);
            return View(preturi);
        }

        // GET: Preturi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preturi preturi = db.Preturi.Find(id);
            if (preturi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", preturi.IdProdus);
            return View(preturi);
        }

        // POST: Preturi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valoare,DataActualizare,EsteUtilizatAcum,IdProdus")] Preturi preturi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preturi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", preturi.IdProdus);
            return View(preturi);
        }

        // GET: Preturi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preturi preturi = db.Preturi.Find(id);
            if (preturi == null)
            {
                return HttpNotFound();
            }
            return View(preturi);
        }

        // POST: Preturi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preturi preturi = db.Preturi.Find(id);
            db.Preturi.Remove(preturi);
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
