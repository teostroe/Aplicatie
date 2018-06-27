using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicentaApp.Domain.Services.Reports;

namespace LicentaApp.Controllers
{
    public class RapoarteController : Controller
    {
        private string _stringDateFormat = "yyyy-MM-dd";
        private readonly LicentaDbContext db = new LicentaDbContext();

        public ActionResult TopLentileVandute()
        {
            return View(this.db.GetTopLentile());
        }

        public ActionResult StatisticaMagazineDupaCantitati()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaCantitati(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        public ActionResult StatisticaMagazineDupaCantitati_Grafic()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaCantitati(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        [HttpGet]
        public ActionResult GetStatisticaMagazineDupaCantitati_Data(DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetStatisticaMagazineDupaCantitati(startDate.ToString(this._stringDateFormat),
                    endDate.ToString(this._stringDateFormat)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopOchelariSoare_CantitatiVandute()
        {
            var dates = InitialDateService.GetInitialDates();
            
            return View(this.db.GetTopOchelariSoare(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        public ActionResult TopOchelariSoare_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/TopOchelariSoare_Data", 
                this.db.GetTopOchelariSoare(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult StatisticaMagazinDupaVanzari()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaVanzari(dates.GetStartDateAsString(),
                dates.GetFinalDateAsString()));
        }

        public ActionResult StatisticaMagazinDupaVanzari_Grafic()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaVanzari(dates.GetStartDateAsString(),
                dates.GetFinalDateAsString()));
        }


        public ActionResult StatisticaMagazinDupaVanzari_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/StatisticaMagazinDupaVanzari_Data",
                this.db.GetStatisticaMagazineDupaVanzari(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult StatisticaMagazinDupaVanzari_GraficData(DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetStatisticaMagazineDupaVanzari(startDate.ToString(this._stringDateFormat),
                    endDate.ToString(this._stringDateFormat)), JsonRequestBehavior.AllowGet);
        }
    }
}