using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Domain.Services
{
    public static class OptionsGenerationService
    {
        public static SelectList GenerateSelectListForValues(double minValue, double maxValue, double step)
        {
            var values = new List<SelectListItem>();
            for (double currentValue = minValue; currentValue  <= maxValue; currentValue += step)
            {
                values.Add(new SelectListItem
                {
                    Text = currentValue.ToString(),
                    Value = currentValue.ToString()
                });
            }
            return new SelectList(values,"Text", "Value");
        }

        public static SelectList GenerateSelectListForValues(int minValue, int maxValue, int step)
        {
            var values = new List<SelectListItem>();
            for (double currentValue = minValue; currentValue <= maxValue; currentValue += step)
            {
                values.Add(new SelectListItem
                {
                    Text = currentValue.ToString(),
                    Value = currentValue.ToString()
                });
            }
            return new SelectList(values, "Text", "Value");
        }
    }
}