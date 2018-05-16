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

namespace LicentaApp.Controllers
{
    public class FurnizoriController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Furnizori
        public ActionResult Index()
        {
            return View(db.Furnizori.ToList());
        }

        // GET: Furnizori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furnizori furnizori = db.Furnizori.Find(id);
            if (furnizori == null)
            {
                return HttpNotFound();
            }
            return View(furnizori);
        }

        // GET: Furnizori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furnizori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Furnizori furnizori)
        {
            if (ModelState.IsValid)
            {
                db.Furnizori.Add(furnizori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(furnizori);
        }

        // GET: Furnizori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furnizori furnizori = db.Furnizori.Find(id);
            if (furnizori == null)
            {
                return HttpNotFound();
            }
            return View(furnizori);
        }

        // POST: Furnizori/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denumire,CUI,CIF,Email,NumarTelefon,Adresa")] Furnizori furnizori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(furnizori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(furnizori);
        }

        // GET: Furnizori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furnizori furnizori = db.Furnizori.Find(id);
            if (furnizori == null)
            {
                return HttpNotFound();
            }
            return View(furnizori);
        }

        // POST: Furnizori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Furnizori furnizori = db.Furnizori.Find(id);
            db.Furnizori.Remove(furnizori);
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
