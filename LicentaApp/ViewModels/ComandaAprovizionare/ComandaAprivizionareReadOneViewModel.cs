using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels.ComandaAprovizionare
{
    public class ComandaAprivizionareReadOneViewModel
    {
        public int NumarComanda { get; set; }
        public int? DeLaFurnizorId { get; set; }
        public int? DeLaDepozitCentralId { get; set; }
        public int? CatreDepozitCentralId { get; set; }
        public int? CatreMagazinId { get; set; }
        public string Expeditor { get; set; }
        public string Destinatar { get; set; }
        public DateTime DataCreare { get; set; }
        public DateTime? DataPrimire { get; set; }
        public StatusComanda Status { get; set; }
        public ComandaAprovizionareProdus[] Produse { get; set; }
    }

    public class ComandaAprovizionareProdus
    {
        public int IdProdus { get; set; }
        public string Cod { get; set; }
        public string Denumire { get; set; }
        public int CantitateCeruta { get; set; }
        public int? CantitatePrimita { get; set; }
    }
}