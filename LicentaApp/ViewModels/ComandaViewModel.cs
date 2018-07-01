using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels
{
    public class ComandaViewModel
    {
        public LicentaApp.Clienti Client { get; set; }
        public ViziteMedicale VizitaMedicala { get; set; }
        public TipComanda? TipComanda { get; set; }
        public decimal? Discount { get; set; }
        public string CodProdusRO { get; set; }
        public decimal? Manopera { get; set; }

        public LentileViewModel Lentila { get; set; }
    }
}