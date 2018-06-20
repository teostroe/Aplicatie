using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Domain.Metadata
{
    public partial class ProductMetadata
    {
        public static readonly List<ProductMetadata> Cache = new List<ProductMetadata>();

        static ProductMetadata()
        {
            ProductMetadata.Initialize();
        }

        public static void Initialize()
        {
            Cache.Clear();

            Cache.Add(new ProductMetadata(ProductProperties.TipLentila, "Tip Lentila", typeof(TipLentila), new[] { TipProdus.Lentile }, Deserializers.EnumDeserializer<TipLentila>, Serializers.EnumSerializer<TipLentila>));
            Cache.Add(new ProductMetadata(ProductProperties.IndiceRefractie, "Indice Refractie", typeof(decimal), new[] { TipProdus.Lentile }, Deserializers.DecimalDeserializer, Serializers.DecimalSerializer));
            //Cache.Add(new ProductMetadata(ProductProperties.Diopetrie, "Dioptrie", typeof(decimal), new[] { TipProdus.Lentile }, Deserializers.DecimalDeserializer, Serializers.DecimalSerializer));
            Cache.Add(new ProductMetadata(ProductProperties.IntervalMin, "Min. Interval", typeof(int), new [] {TipProdus.Rame, TipProdus.OchelariSoare},
                Deserializers.IntegerDeserializer, Serializers.IntegerSerializer));
            Cache.Add(new ProductMetadata(ProductProperties.IntervalMax, "Max. Interval", typeof(int), new[] { TipProdus.Rame, TipProdus.OchelariSoare },
                Deserializers.IntegerDeserializer, Serializers.IntegerSerializer));
            Cache.Add(new ProductMetadata(ProductProperties.SablonMin, "Min. Sablon", typeof(int), new[] { TipProdus.Rame, TipProdus.OchelariSoare },
                Deserializers.IntegerDeserializer, Serializers.IntegerSerializer));
            Cache.Add(new ProductMetadata(ProductProperties.SablonMax, "Max. Sablon", typeof(int), new[] { TipProdus.Rame, TipProdus.OchelariSoare },
                Deserializers.IntegerDeserializer, Serializers.IntegerSerializer));
            Cache.Add(new ProductMetadata(ProductProperties.EstePolarizat, "Este Polarizat", typeof(DaNu), new [] {TipProdus.OchelariSoare },
                Deserializers.BooleanDeserialzier, Serializers.BooleanSerializer));
        }

        public static List<ProductMetadata> GetAllForProductType(TipProdus tipProdus)
        {
            return Cache.Where(x => x.ProductTypes.Any(y => y == tipProdus)).ToList();
        }

        public static ProductMetadata GetForProperty(string property)
        {
            return Cache.Single(x => x.PropertyName == property);
        }
    }

}