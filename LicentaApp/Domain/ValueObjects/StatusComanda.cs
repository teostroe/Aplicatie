using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum StatusComanda
    {
        [Display(Name = "Creata")]
        Creata = 1,
        [Display(Name = "In Tranzit")]
        InTranzit = 2,
        [Display(Name = "In Prelucrare")]
        InPrelucrare = 3,
        [Display(Name = "In Tranzit Magazin")]
        InTranzitMagazin = 4,
        [Display(Name = "Trimisa in Magazin")]
        TrimisaInMagazin = 5,
        [Display(Name = "Finalizata")]
        Finalizata = 6
        
    }
}