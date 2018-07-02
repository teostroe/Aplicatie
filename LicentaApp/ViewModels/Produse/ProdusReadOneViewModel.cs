using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Produse
{
    public class ProdusReadOneViewModel
    {
        public LicentaApp.Produse Produs { get; set; }
        public ICollection<DetaliiProdus> DetaliiProdus { get; set; }
        public int InventarMagazin { get; set; }
        public int InventarDepozitCentral { get; set; }
    }
}