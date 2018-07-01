using System;
using System.Collections.Generic;
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
    public class MagazineController : BaseAppController
    {
        // GET: Magazine
        public ActionResult Index(int? page)
        {
            var model = db.Magazine;
            ViewData.InitializePagination(page, model.Count(), this.ControllerContext);
            return View(model);
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
            return View(new Magazine());
        }

        // POST: Magazine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Magazine magazine)
        {
            if (!ModelState.IsValid)
            {
                return View(magazine);
                
            }

            db.Magazine.Add(magazine);
            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return View(magazine);
            }

            return RedirectToAction("Index");

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
        public ActionResult Edit(Magazine magazine)
        {
            ViewBag.Action = AppConstants.CRUD.Edit;
            if (!ModelState.IsValid)
            {
                return View(magazine);
            }

            db.Entry(magazine).State = EntityState.Modified;
            var result = this.SaveChanges();
            if (result != DbSaveResult.Success)
            {
                return View(magazine);
            }
            return RedirectToAction("Index");

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
