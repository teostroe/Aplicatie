using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicentaApp;
using LicentaApp.Controllers.Base;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
    public class FurnizoriController : BaseAppController
    {
        // GET: Furnizori
        public ActionResult Index(int? page)
        {
            var model = db.Furnizori;
            ViewData.InitializePagination(page,model.Count(), this.ControllerContext);
            return View(model.ToPagedList(page));
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
            if (!ModelState.IsValid)
            {
                return View(furnizori);
            }
            db.Furnizori.Add(furnizori);
            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return View(furnizori);
            }

            return RedirectToAction("Index");


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
        public ActionResult Edit(Furnizori furnizori)
        {
            if (!ModelState.IsValid)
            {
                return View(furnizori);
            }

            db.Entry(furnizori).State = EntityState.Modified;
            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return View(furnizori);
            }
            return RedirectToAction("Index");
        }

        // GET: Furnizori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (this.db.ComenziAprovizionari.Any(x => x.DeLaFurnizorId == id) || this.db.Produse.Any(x => x.IdFurnizor == id))
            {
                TempData.Add(AppConstants.Alerts.Error, new[] { new ValidationResult("Furnizorul nu poate fi sters deoarece a executat deja operatii") });
                return RedirectToAction("Details", new { id = id });
            }

            Furnizori furnizori = db.Furnizori.Find(id);
            db.Furnizori.Remove(furnizori);

            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return RedirectToAction("Details", new { id = id });
            }
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
