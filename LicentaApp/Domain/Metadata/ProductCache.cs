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
            Cache.Add(new ProductMetadata(ProductProperties.TipTratament, "Tip Tratament", typeof(TipTratament), new[] { TipProdus.Lentile }, Deserializers.EnumDeserializer<TipTratament>, Serializers.EnumSerializer<TipTratament>));
            Cache.Add(new ProductMetadata(ProductProperties.Diopetrie, "Dioptrie", typeof(decimal), new[] { TipProdus.Lentile }, Deserializers.DecimalDeserializer, Serializers.DecimalSerializer));
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