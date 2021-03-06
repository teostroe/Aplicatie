﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    [Authorize(Roles = AuthConstants.Permisii.AdminUtilizator)]
    public class UtilizatoriController : BaseAppController
    {
        private LicentaDbContext db = new LicentaDbContext();

        // GET: Utilizatori
        public ActionResult Index(int? page)
        {
            var utilizatori = db.Utilizatori.Include(u => u.Magazine).Include(u => u.Roluri);
            ViewData.InitializePagination(page, utilizatori.Count(), this.ControllerContext);
            return View(utilizatori.ToPagedList(page));
        }

        // GET: Utilizatori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizatori utilizatori = db.Utilizatori.Find(id);
            if (utilizatori == null)
            {
                return HttpNotFound();
            }
            return View(utilizatori);
        }


        // GET: Utilizatori/Inregistreaza
        public ActionResult Inregistreaza()
        {
            this.InitIngreistreazaSelectList();
            return View();
        }

        // POST: Utilizatori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Inregistreaza(Utilizatori utilizator)
        {
            if (!ModelState.IsValid)
            {
                this.InitIngreistreazaSelectList();
                return View(utilizator);
            }
            try
            {
                db.Utilizatori.Add(utilizator);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                this.InitIngreistreazaSelectList();
                ModelState.AddModelError(string.Empty, SqlExceptionService.GetHandledSqlError(ex));
                return View(utilizator);
            }

            return RedirectToAction("Index");
        }

        private void InitIngreistreazaSelectList()
        {
            ViewBag.MagazineSelectList = db.Magazine.ToSelectList(x => x.Id, x => x.Denumire);
            ViewBag.RoluriSelectList = db.Roluri.ToSelectList(x => x.Id, x => x.Denumire);

            //ViewBag.MagazineSelectList = new SelectList(db.Magazine, "Id", "Denumire");
            //ViewBag.RoluriSelectList = new SelectList(db.Roluri, "Id", "Denumire");
        }

        // GET: Utilizatori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizatori utilizatori = db.Utilizatori.Find(id);
            if (utilizatori == null)
            {
                return HttpNotFound();
            }
            this.InitIngreistreazaSelectList();

            return View(utilizatori);
        }

        // POST: Utilizatori/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Nume,Prenume,Parola,Email,IdRol,IdMagazin")] Utilizatori utilizatori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizatori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMagazin = new SelectList(db.Magazine, "Id", "Denumire", utilizatori.IdMagazin);
            ViewBag.IdRol = new SelectList(db.Roluri, "Id", "Denumire", utilizatori.IdRol);
            return View(utilizatori);
        }

        // GET: Utilizatori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (this.db.Comenzi.Any(x => x.IdUtilizator == id) || this.db.ComenziAprovizionari.Any(x => x.IdUtilizator == id))
            {
                TempData.Add(AppConstants.Alerts.Error, new[] { new ValidationResult("Utilizatorul nu poate fi sters deoarece a executat deja operatii") });
                return RedirectToAction("Details", new { id = id });
            }

            var utilizator = db.Utilizatori.Find(id);
            db.Utilizatori.Remove(utilizator);
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
