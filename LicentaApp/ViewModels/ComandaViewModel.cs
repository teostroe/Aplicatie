using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels
{
    public class ComandaViewModel
    {
        public Clienti Client { get; set; }
        //public Comenzi Comanda { get; set; }
        public ViziteMedicale VizitaMedicala { get; set; }
        public LentileViewModel Lentila { get; set; }

        public string CodRama { get; set; }
        public decimal DiscountRama { get; set; }
    }
}