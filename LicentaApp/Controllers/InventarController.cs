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
    public class InventarController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Inventar
        public ActionResult Index()
        {
            var inventar = db.Inventar.Include(i => i.Magazine).Include(i => i.Produse);
            return View(inventar.ToList());
        }

        // GET: Inventar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventar inventar = db.Inventar.Find(id);
            if (inventar == null)
            {
                return HttpNotFound();
            }
            return View(inventar);
        }

        // GET: Inventar/Create
        public ActionResult Create()
        {
            ViewBag.IdMagazin = new SelectList(db.Magazine, "Id", "Denumire");
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod");
            return View();
        }

        // POST: Inventar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CantitateDisponibila,IdMagazin,IdProdus")] Inventar inventar)
        {
            if (ModelState.IsValid)
            {
                db.Inventar.Add(inventar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMagazin = new SelectList(db.Magazine, "Id", "Denumire", inventar.IdMagazin);
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", inventar.IdProdus);
            return View(inventar);
        }

        // GET: Inventar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventar inventar = db.Inventar.Find(id);
            if (inventar == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMagazin = new SelectList(db.Magazine, "Id", "Denumire", inventar.IdMagazin);
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", inventar.IdProdus);
            return View(inventar);
        }

        // POST: Inventar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CantitateDisponibila,IdMagazin,IdProdus")] Inventar inventar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMagazin = new SelectList(db.Magazine, "Id", "Denumire", inventar.IdMagazin);
            ViewBag.IdProdus = new SelectList(db.Produse, "Id", "Cod", inventar.IdProdus);
            return View(inventar);
        }

        // GET: Inventar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventar inventar = db.Inventar.Find(id);
            if (inventar == null)
            {
                return HttpNotFound();
            }
            return View(inventar);
        }

        // POST: Inventar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventar inventar = db.Inventar.Find(id);
            db.Inventar.Remove(inventar);
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
