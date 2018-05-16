using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Metadata
{
    public partial class ProductMetadata
    {
        public static class Serializers
        {
            public static string StringSerializer(object obj)
            {
                return (string)obj;
            }

            public static string DecimalSerializer(object obj)
            {
                return obj != null ? ((decimal)obj).ToString().ToUpper() : null;
            }
                
            public static string EnumSerializer<T>(object obj) where T : struct
            {
                if (!typeof(T).IsEnum) { throw new NotSupportedException("The type parameter must be an enumeration"); }
                return obj != null ? Convert.ChangeType(obj, Enum.GetUnderlyingType(typeof(T))).ToString() : null;
            }
        }

        public static class Deserializers
        {
            public static object StringDeserializer(string str)
            {
                return str;
            }

            public static object DecimalDeserializer(string str)
            {
                return str != null ? decimal.Parse(str) : default(decimal?);
            }

            public static object EnumDeserializer<T>(string str) where T : struct
            {
                if (!typeof(T).IsEnum) { throw new NotSupportedException("The type parameter must be an enumeration"); }
                return str != null ? (T)Enum.Parse(typeof(T), str) : default(Nullable<T>);
            }
        }
    }
}