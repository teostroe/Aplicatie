using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.Metadata;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.ViewModels
{
    public class ProduseViewModel
    {
        public Produse Produse { get; set; }
        public decimal Pret { get; set; }
        public List<ProductMetadata> ProductMetadata { get; set; }
        public Dictionary<string, string> ProductProperties { get; set; }
        public bool Test { get; set; }
    }
}