using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LicentaApp.Domain;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Services.Reports;
using LicentaApp.ViewModels.Rapoarte;

namespace LicentaApp.Controllers
{
    [Authorize(Roles = AuthConstants.Permisii.AdminOnly)]
    public class RapoarteController : Controller
    {
        private string _stringDateFormat = "yyyy-MM-dd";
        private readonly LicentaDbContext db = new LicentaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Top Lentile 

        public ActionResult TopLentileVandute()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetTopLentile(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        public ActionResult TopLentileVandute_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/TopLentileVandute_Data",
                this.db.GetTopLentile(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        #endregion

        #region Top Rame 

        public ActionResult TopRameVandute()
        {
            var dates = InitialDateService.GetInitialDates();
            return View(this.db.GetTopRame(dates.GetStartDateAsString(), dates.GetFinalDateAsString()));
        }

        public ActionResult TopRameVandute_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/TopRameVandute_Data",
                this.db.GetTopRame(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        #endregion

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
        public ActionResult TopProfesii()
        {
            ViewData.Add(AppConstants.OraseOptions, new SelectList(db.Magazine.Select(x => x.Oras).Distinct()));
            return View();
        }

        public ActionResult TopProfesii_Data(string oras, DateTime startDate, DateTime endDate)
        {
            var dates = InitialDateService.GetInitialDates();
            return PartialView("Rapoarte/TopProfesii_Data",
                this.db.GetTopProfesiiClienti(
                    oras,
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult TopProfesii_Grafic()
        {
            ViewData.Add(AppConstants.OraseOptions, new SelectList(db.Magazine.Select(x => x.Oras).Distinct()));
            return View();
        }
        [HttpGet]
        public ActionResult TopProfesii_GraficData(string oras, DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetTopProfesiiClienti(
                    oras,
                    startDate.ToString(this._stringDateFormat),
                    endDate.ToString(this._stringDateFormat)), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Statistica Magazine dupa Cantitati
        public ActionResult StatisticaMagazineDupaCantitati()
        {
            return View();
        }

        public ActionResult StatisticaMagazineDupaCantitati_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/StatisticaMagazineDupaCantitati_Data",
                this.db.GetStatisticaMagazineDupaCantitati(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult StatisticaMagazineDupaCantitati_Grafic()
        {
            return View();
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
            return View();
        }

        public ActionResult StatisticaMagazinDupaVanzari_Grafic()
        {
            return View();
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

        #region Statistica Utilizatori dupa Cantitati
        public ActionResult StatisticaUtilizatoriDupaCantitati()
        {
            return View();
        }

        public ActionResult StatisticaUtilizatoriDupaCantitati_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/StatisticaUtilizatoriDupaCantitati_Data",
                this.db.GetStatisticaUtilizatoriDupaCantitati(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult StatisticaUtilizatoriDupaCantitati_Grafic()
        {
            return View();
        }
        [HttpGet]
        public ActionResult StatisticaUtilizatoriDupaCantitati_GraficData(DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetStatisticaUtilizatoriDupaCantitati(startDate.ToString(this._stringDateFormat),
                    endDate.ToString(this._stringDateFormat)), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Statistica Utilizatori Dupa Vanzari
        public ActionResult StatisticaUtilizatoriDupaVanzari()
        {
            return View();
        }

        public ActionResult StatisticaUtilizatoriDupaVanzari_Data(DateTime startDate, DateTime endDate)
        {
            return PartialView("Rapoarte/StatisticaUtilizatoriDupaVanzari_Data",
                this.db.GetStatisticaUtilizatoriDupaVanzari(
                    startDate.ToString(_stringDateFormat),
                    endDate.ToString(_stringDateFormat)));
        }

        public ActionResult StatisticaUtilizatoriDupaVanzari_Grafic()
        {
            return View();
        }

        public ActionResult StatisticaUtilizatoriDupaVanzari_GraficData(DateTime startDate, DateTime endDate)
        {
            return Json(
                this.db.GetStatisticaUtilizatoriDupaVanzari(startDate.ToString(this._stringDateFormat),
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
                Data = dataRows.OrderBy(x => this.GetDateFromInterval(x.Interval)).ToArray()
            };

            return Json(result
               , JsonRequestBehavior.AllowGet);

        }

        private DateTime? GetDateFromInterval(string date)
        {
            if (!date.Contains("-"))
            {
                return null;
            }

            var vals = date.Split('-');
            return new DateTime(Convert.ToInt32(vals[0]), Convert.ToInt32(vals[1]), 1);
        }
        #endregion

        #region Istoric Preturi
        public ActionResult IstoricPreturi_Grafic()
        {
            ViewData.Add(AppConstants.ProduseOptions, this.db.Produse.ToSelectList(x => x.Cod, x => x.Denumire));
            return View();
        }

        public ActionResult IstoricPreturi_GraficData(string codProdus)
        {
            var model =
                this.db.GetIstoricPreturi(codProdus)
                    .ToList().Select(x => new
                    {
                        Data = x.DataActualizare.ToString(_stringDateFormat),
                        Valoare = x.Valoare
                    }).ToList();

            model.Add(new
                {
                Data = DateTime.Now.ToString(_stringDateFormat),
                Valoare = model.LastOrDefault().Valoare
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}