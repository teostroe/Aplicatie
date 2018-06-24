using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels.Comanda
{
    public class ProdusVerificareViewModel
    {
        public string DenumireProduse { get; set; }
        public decimal? Discount { get; set; }
        public decimal Pret { get; set; }
        public int Cantitate { get; set; }
        public TipProdus TipProdus { get; set; }
    }
}