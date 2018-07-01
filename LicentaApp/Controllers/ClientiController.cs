using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicentaApp;
using System.Data.Entity.Core.Objects;
using LicentaApp.Domain.Auth;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class ClientiController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Clienti
        public ActionResult Index()
        {
            return View(db.Clienti.ToList());
        }

        // GET: Clienti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // GET: Clienti/Create
        public ActionResult Create()
        {
            TempData.Add("TestObj", "TEST");
            return View();
        }

        // POST: Clienti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nume,Prenume,NumarTelefon,Email,DataNastere,DataInregistrare,Profesie")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Clienti.Add(clienti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clienti);
        }

        // GET: Clienti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // POST: Clienti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nume,Prenume,NumarTelefon,Email,DataNastere,DataInregistrare,Profesie")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienti);
        }

        // GET: Clienti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // POST: Clienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clienti clienti = db.Clienti.Find(id);
            db.Clienti.Remove(clienti);
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
