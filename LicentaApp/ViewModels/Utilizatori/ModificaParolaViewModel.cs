using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Utilizatori
{
    public class ModificaParolaViewModel
    {
        public string Username { get; set; }
        public string ParolaCurenta { get; set; }
        public string ParolaNoua { get; set; }
        public string ConfirmaParolaNoua { get; set; }
    }
}