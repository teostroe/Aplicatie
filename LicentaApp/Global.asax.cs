using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Auth.DTO;
using LicentaApp.Domain.Services.Binders;
using Newtonsoft.Json;

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

            var enCultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = enCultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = enCultureInfo;
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[AuthConstants.UserAuthCookie];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<SerializeUser>(authTicket.UserData);

                AppPrincipal principal = new AppPrincipal(authTicket.Name);

                principal.UserId = serializeModel.UserId;
                principal.Nume = serializeModel.Nume;
                principal.Prenume = serializeModel.Prenume;
                principal.Rol = serializeModel.DenumireRol;
                principal.IdMagazin = serializeModel.IdMagazin;
                HttpContext.Current.User = principal;
            }

        }
    }
}
