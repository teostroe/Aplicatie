using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LicentaApp.Domain.Services.Binders;

namespace LicentaApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
            var enCultureInfo = new CultureInfo("en-US");
            //enCultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //var roCultureInfo = new CultureInfo("ro-RO");
            //enCultureInfo.DateTimeFormat = roCultureInfo.DateTimeFormat;
            CultureInfo.DefaultThreadCurrentCulture = enCultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = enCultureInfo;
        }
    }
}
