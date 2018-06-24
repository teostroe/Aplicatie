using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LicentaApp.Domain.Services.Binders
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == null)
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            DateTime result;
            DateTime.TryParseExact(valueProviderResult.AttemptedValue,
                "dd/MM/yyyy", 
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, 
                out result);
            return result;
        }
    }
}