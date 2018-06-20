using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum TipLentila
    {
        [Display(Name = "Monofocala Uniforma")]
        MonofocalaUniforma = 1,
        [Display(Name = "Bifocala")]
        Bifocala = 2,
        [Display(Name = "Progresiva")]
        Progresiva = 3,
        [Display(Name = "Minerala")]
        Minerala = 4
    }
}