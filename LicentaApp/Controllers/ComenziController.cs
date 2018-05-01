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
    public class ComenziController : Controller
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Comenzi
        public ActionResult Index()
        {
            var comenzi = db.Comenzi.Include(c => c.Clienti).Include(c => c.Utilizatori).Include(c => c.ViziteMedicale);
            return View(comenzi.ToList());
        }

        // GET: Comenzi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            return View(comenzi);
        }

        // GET: Comenzi/Create
        public ActionResult Create()
        {
            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume");
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username");
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala");
            return View();
        }

        // POST: Comenzi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,Discount,IdUtilizator,IdClient")] Comenzi comenzi)
        {
            if (ModelState.IsValid)
            {
                db.Comenzi.Add(comenzi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume", comenzi.IdClient);
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username", comenzi.IdUtilizator);
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala", comenzi.Id);
            return View(comenzi);
        }

        // GET: Comenzi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume", comenzi.IdClient);
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username", comenzi.IdUtilizator);
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala", comenzi.Id);
            return View(comenzi);
        }

        // POST: Comenzi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Discount,IdUtilizator,IdClient")] Comenzi comenzi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comenzi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "Id", "Nume", comenzi.IdClient);
            ViewBag.IdUtilizator = new SelectList(db.Utilizatori, "Id", "Username", comenzi.IdUtilizator);
            ViewBag.Id = new SelectList(db.ViziteMedicale, "IdComandaVizitaMedicala", "IdComandaVizitaMedicala", comenzi.Id);
            return View(comenzi);
        }

        // GET: Comenzi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comenzi comenzi = db.Comenzi.Find(id);
            if (comenzi == null)
            {
                return HttpNotFound();
            }
            return View(comenzi);
        }

        // POST: Comenzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comenzi comenzi = db.Comenzi.Find(id);
            db.Comenzi.Remove(comenzi);
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
