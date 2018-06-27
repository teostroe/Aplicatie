using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Services.Reports
{
    public class ReportDates
    {
        private const string _stringDateFormat = "yyyy-MM-dd";
        private const string _uiStringDateFormat = "dd/MM/yyyy";

        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }

        public string GetStartDateAsString()
        {
            return this.StartDate.ToString(_stringDateFormat);
        }

        public string GetFinalDateAsString()
        {
            return this.FinalDate.ToString(_stringDateFormat);
        }

        public string GetStartDateUiFormatted()
        {
            return this.StartDate.ToString(_uiStringDateFormat);
        }

        public string GetFinalDateUiFormatted()
        {
            return this.FinalDate.ToString(_uiStringDateFormat);
        }

    }
}