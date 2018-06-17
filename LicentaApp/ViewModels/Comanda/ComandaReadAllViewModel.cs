using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Comanda
{
    public class ComandaReadAllViewModel
    {
        public int NumarComanda { get; set; }
        public string NumeClient { get; set; }
        public string NumeAngajat { get; set; }
        public DateTime Data { get; set; }
    }
}