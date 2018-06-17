using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum TipProdus
    {
        [Display(Name="Lentile")]
        Lentile = 1,
        [Display(Name = "Rame")]
        Rame = 2,
        [Display(Name = "Ochelari de Soare")]
        OchelariSoare = 3
    }
}