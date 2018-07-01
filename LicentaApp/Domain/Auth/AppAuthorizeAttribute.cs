using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicentaApp.Domain.Auth
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {

        protected virtual AppPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as AppPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            routeData = new RedirectToRouteResult
            (new System.Web.Routing.RouteValueDictionary
            (new
            {
                controller = "Account",
                action = "Login",
            }
            ));

            filterContext.Result = routeData;
        }
    }
}