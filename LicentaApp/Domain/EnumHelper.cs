using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LicentaApp.Domain
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Type enumType, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (enumType == null)
            {
                return string.Empty;
            }

            var name = Enum.GetName(enumType, value);
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            var fi = enumType.GetField(name);

            var descriptionAttributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].Description;
            }

            var displayAttributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (displayAttributes.Length > 0)
            {
                return displayAttributes[0].GetName();
            }

            name = Regex.Replace(name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ").TrimEnd();

            return name;
        }
    }
}