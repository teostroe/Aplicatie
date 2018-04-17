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
    public class ProduseController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Produse
        public ActionResult Index()
        {
            var produse = db.Produse.Include(p => p.Furnizori);
            return View(produse.ToList());
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

        // GET: Produse/Create
        public ActionResult Create()
        {
            ViewBag.IdFurnizor = new SelectList(db.Furnizori, "Id", "Denumire");
            return View();
        }

        // POST: Produse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cod,Denumire,Discount,TipProdus,IdFurnizor")] Produse produse)
        {
            if (ModelState.IsValid)
            {
                db.Produse.Add(produse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFurnizor = new SelectList(db.Furnizori, "Id", "Denumire", produse.IdFurnizor);
            return View(produse);
        }

        // GET: Produse/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdFurnizor = new SelectList(db.Furnizori, "Id", "Denumire", produse.IdFurnizor);
            return View(produse);
        }

        // POST: Produse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cod,Denumire,Discount,TipProdus,IdFurnizor")] Produse produse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFurnizor = new SelectList(db.Furnizori, "Id", "Denumire", produse.IdFurnizor);
            return View(produse);
        }

        // GET: Produse/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Produse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produse produse = db.Produse.Find(id);
            db.Produse.Remove(produse);
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
