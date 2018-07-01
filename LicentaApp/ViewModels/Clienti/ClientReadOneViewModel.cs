using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Clienti
{
    public class ClientReadOneViewModel
    {
        public LicentaApp.Clienti Client;
        public IEnumerable<ViziteMedicaleReadOneViewModel> ViziteMedicale { get; set; }
    }

    public class ViziteMedicaleReadOneViewModel
    {
        public ViziteMedicale VizitaMedicala { get; set;  }

        public DateTime Data { get; set; }
    }
}