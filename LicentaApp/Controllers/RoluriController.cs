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
    public class RoluriController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Roluri
        public ActionResult Index()
        {
            return View(db.Roluri.ToList());
        }

        // GET: Roluri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roluri roluri = db.Roluri.Find(id);
            if (roluri == null)
            {
                return HttpNotFound();
            }
            return View(roluri);
        }

        // GET: Roluri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roluri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denumire")] Roluri roluri)
        {
            if (ModelState.IsValid)
            {
                db.Roluri.Add(roluri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roluri);
        }

        // GET: Roluri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roluri roluri = db.Roluri.Find(id);
            if (roluri == null)
            {
                return HttpNotFound();
            }
            return View(roluri);
        }

        // POST: Roluri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denumire")] Roluri roluri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roluri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roluri);
        }

        // GET: Roluri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roluri roluri = db.Roluri.Find(id);
            if (roluri == null)
            {
                return HttpNotFound();
            }
            return View(roluri);
        }

        // POST: Roluri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roluri roluri = db.Roluri.Find(id);
            db.Roluri.Remove(roluri);
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
