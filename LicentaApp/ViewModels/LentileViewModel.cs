using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels
{
    public class LentileViewModel
    {
        public TipLentila TipLentila { get; set; }
        public string CodProdus { get; set; }
        public string Tratement { get; set; }
    }
}