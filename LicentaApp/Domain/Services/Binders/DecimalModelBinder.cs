using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Domain.Services.Binders
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == null || valueProviderResult.AttemptedValue.IsNullOrEmpty())
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            decimal result;
            Decimal.TryParse(valueProviderResult.AttemptedValue, out result);
            return result;
        }
    }
}