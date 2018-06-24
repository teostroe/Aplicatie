using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Controllers
{
    public class ComenziAprovizionareController : Controller
    {

        private LicentaDbContext db = new LicentaDbContext();
        // GET: ComenziAprovizionare
        public ActionResult ComenziAprovizionareFurnizori()
        {
            //db.ComenziAprovizionari
            //    .Where(x => x.DeLaFurnizorId.HasValue)
            //    .Select()
            return View();
        }

        public ActionResult ComenziAprovizionareMagazine()
        {
            return View();
        }
    }
}