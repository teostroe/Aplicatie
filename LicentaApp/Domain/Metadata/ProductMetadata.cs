using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Domain.Metadata
{
    public partial class ProductMetadata
    {
        public ProductMetadata(
            string propertyName,
            string propertyDisplayName,
            Type dataType,
            TipProdus[] productTypes,
            Func<string, object> deserializer,
            Func<object, string> serializer 
        )
        {
            this.PropertyName = propertyName;
            this.PropertyDisplayName = propertyDisplayName;
            this.DataType = dataType;
            this.ProductTypes = productTypes;
            this.Deserializer = deserializer;
            this.Serializer = serializer;
        }

        public string PropertyName { get; set; }
        public string PropertyDisplayName { get; set; }
        public Type DataType { get; set; }
        public TipProdus[] ProductTypes { get; set; }
        public Func<string, object> Deserializer { get; set; }
        public Func<object, string> Serializer { get; set; }

    }
}