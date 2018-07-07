﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.ViewModels.Rapoarte.Search
{
    public class OchelariSoareFilter
    {
        public string Cod { get; set; }
        public string Denumire { get; set; }
        public decimal? Discount { get; set; }
        public string EstePolarizat { get; set; }
        public decimal? Pret { get; set; }

        public int? Page { get; set; }

    }
}