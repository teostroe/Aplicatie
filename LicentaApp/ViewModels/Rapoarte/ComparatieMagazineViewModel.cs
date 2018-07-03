using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Rapoarte
{
    public class ComparatieMagazineViewModel
    {
        public string[] Headers { get; set; }
        public ComparatieMagazinDataRow[] Data { get; set; }
    }

    public class ComparatieMagazinDataRow
    {
        public string Interval { get; set; }
        public decimal[] Valori { get; set; }
    }
}