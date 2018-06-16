using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LicentaApp.Domain.Metadata;
using LicentaApp.Domain.ValueObjects;

namespace LicentaApp.Domain.Services
{
    public class SerializationService
    {
        public static Dictionary<string, string> SerializeProductData(Dictionary<string, object> data)
        {
            var result = new Dictionary<string, string>();
            foreach (var item in data)
            {
                result.Add(item.Key, ProductMetadata.GetForProperty(item.Key).Serializer(item.Value));
            }

            return result;
        }
    }
}