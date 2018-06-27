using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Services.Reports
{
    public class InitialDateService
    {
        public static ReportDates GetInitialDates()
        {
            var today = DateTime.Now;
            return new ReportDates
            {
                StartDate = today.AddYears(-1),
                FinalDate = today
            };
        }
    }
}