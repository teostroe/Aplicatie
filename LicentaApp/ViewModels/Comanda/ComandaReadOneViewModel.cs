using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels.Comanda
{
    public class ComandaReadOneViewModel
    {
        public int NumarComanda { get; set; }
        public DateTime Data { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public StatusComanda StatusComanda { get; set; }
        public LicentaApp.Clienti Client { get; set; }
        public ViziteMedicale VizitaMedicala { get; set; }
        public IEnumerable<ComandaProdusViewModel> Produse { get; set; }

    }

    public class ComandaClientViewModel
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string NumarTelefon { get; set; }
        public string Email { get; set; }
        public DateTime DataNastere { get; set; }
        public DateTime DataInregistrare { get; set; }
        public string Profesie { get; set; }
    }

    public class ComandaVizitaMedicalaViewModel
    {
        public decimal DistantaPupilara { get; set; }
        public decimal? SferaDistantaStang { get; set; }
        public decimal? SferaAproapeStang { get; set; }
        public decimal? CilindruStang { get; set; }
        public decimal? AxStang { get; set; }
        public decimal? PrismaStang { get; set; }
        public decimal? SferaDistantaDrept { get; set; }
        public decimal? SferaAproapeDrept { get; set; }
        public decimal? CilindruDrept { get; set; }
        public decimal? AxDrept { get; set; }
        public decimal? PrismaDrept { get; set; }
    }

    public class ComandaProdusViewModel
    {
        public string Cod { get; set; }
        public string Denumire { get; set; }
        public decimal? Discount { get; set; }
        public TipProdus TipProdus { get; set; }
        public int Cantitate { get; set; }
        public decimal Pret { get; set; }
    }
}