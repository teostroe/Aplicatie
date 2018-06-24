using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels.ComandaAprovizionare
{
    public class ComandaAprivizionareCreate
    {
        public int IdExpeditor { get; set; }
        public string[] Coduri { get; set; }
        public int[] Cantitati { get; set; }
    }
}