using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels.Rapoarte.Search
{
    public class LentileFilter
    {
        public string Cod { get; set; }
        public string Denumire { get; set; }
        public decimal? Discount { get; set; }
        public string TipLentila { get; set; }
        public string IndiceRefractie { get; set; }
        public decimal? Pret { get; set; }

        public int? Page { get; set; }
    }
}