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
    public class MagazineController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Magazine
        public ActionResult Index()
        {
            return View(db.Magazine.ToList());
        }

        // GET: Magazine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazine.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // GET: Magazine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Magazine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denumire,Oras,Adresa,Email")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.Magazine.Add(magazine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magazine);
        }

        // GET: Magazine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazine.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // POST: Magazine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denumire,Oras,Adresa,Email")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magazine);
        }

        // GET: Magazine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazine.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        // POST: Magazine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magazine magazine = db.Magazine.Find(id);
            db.Magazine.Remove(magazine);
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
