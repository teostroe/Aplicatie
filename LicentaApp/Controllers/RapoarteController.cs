using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LicentaApp.Domain;
using LicentaApp.Domain.Services.Reports;
using LicentaApp.ViewModels.Rapoarte;

namespace LicentaApp.Controllers
{
    public class RapoarteController : Controller
    {
        private string _stringDateFormat = "yyyy-MM-dd";
        private readonly LicentaDbContext db = new LicentaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopLentileVandute()
        {
            return View(this.db.GetTopLentile());
        }

        #region Top Ochelari Soare
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

        #endregion

        #region Top Profesii Clienti
        
        #endregion

        #region Statistica Magazine dupa Cantitati
        public ActionResult StatisticaMagazineDupaCantitati()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaCantitati(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        public ActionResult StatisticaMagazineDupaCantitati_Data(DateTime startDate, DateTime endDate)
        {
            var dates = InitialDateService.GetInitialDates();
            return PartialView("Rapoarte/StatisticaMagazineDupaCantitati_Data",
                this.db.GetStatisticaMagazineDupaCantitati(
                    dates.GetStartDateAsString(),
                    dates.GetFinalDateAsString()));
        }

        public ActionResult StatisticaMagazineDupaCantitati_Grafic()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetStatisticaMagazineDupaCantitati(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }
        [HttpGet]
        public ActionResult StatisticaMagazineDupaCantitati_GraficData(DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetStatisticaMagazineDupaCantitati(startDate.ToString(this._stringDateFormat),
                    endDate.ToString(this._stringDateFormat)), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Statistica Magazine Dupa Vanzari
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
        #endregion

        #region Pivot
        public ActionResult VanzariTotalePeLuni()
        {
            return View(this.db.PivotTableTotalVanzari());
        }
        #endregion

        #region Comparatie Magazine dupa Vanzari

        public ActionResult ComparatieMagazineDupaVanzari_Grafic()
        {
            ViewData.Add(AppConstants.MagazineOptions, this.db.Magazine.Where(x => !x.EsteDepozitCentral).ToSelectList(x => x.Id,  x => x.Denumire));
            return View();
        }

        public ActionResult ComparatieMagazineDupaVanzari_GraficData(string magazineIds, DateTime startDate,
            DateTime endDate)
        {
            var array = new JavaScriptSerializer().Deserialize<string[]>(magazineIds);
            var data = this.db.GetComparatieMagazineDupaVanzari(
                array.ToCommaSeparatedString(),
                startDate.ToString(this._stringDateFormat),
                endDate.ToString(this._stringDateFormat))
                .ToList();

            var idDenumireMapping = data.Select(x => new
            {
                x.Id,
                x.Denumire
            }).Distinct().ToDictionary(x => x.Id, x => x.Denumire);
            var headers = new List<string>();
            headers.Add("Data");
            headers.AddRange(idDenumireMapping.Select(x => x.Value).ToArray());
            var grouppedData = data.GroupBy(x => x.Interval);
            var dataRows = grouppedData.Select(x => new ComparatieMagazinDataRow
            {
                Interval = x.Key,
                Valori = idDenumireMapping.Select(y => 
                    x.Any(z => z.Id == y.Key) ? x.FirstOrDefault(z => z.Id == y.Key).TotalVanzari : 0m).ToArray()
            });

            var result = new ComparatieMagazineViewModel
            {
                Headers = headers.ToArray(),
                Data = dataRows.ToArray()
            };

            return Json(result
               , JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}