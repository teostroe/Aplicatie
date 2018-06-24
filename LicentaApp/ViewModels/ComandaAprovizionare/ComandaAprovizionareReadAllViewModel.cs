using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.ComandaAprovizionare
{
    public class ComandaAprovizionareReadAllViewModel
    {
        public int NumarComanda { get; set; }
        public string Expeditor { get; set; }
        public string Destinatar { get; set; }
        public string DataCerere { get; set; }
    }
}