using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum TipComanda
    {
        [Display(Name = "Comanda Simpla")]
        ComandaSimpla = 1,
        [Display(Name = "Comanda cu Prelucrare")]
        ComandaCuPrelucrare = 2
    }
}