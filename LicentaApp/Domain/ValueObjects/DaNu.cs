using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.ValueObjects
{
    public enum DaNu
    {
        [Display(Name = "Nu")]
        Nu = 0,
        [Display(Name = "Da")]
        Da = 1
    }
}