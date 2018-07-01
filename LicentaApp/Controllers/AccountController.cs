using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using LicentaApp.Domain.Auth;
using LicentaApp.Domain.Auth.DTO;
using LicentaApp.ViewModels.Auth;
using Newtonsoft.Json;

namespace LicentaApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    var user = (AppMembershipUser)Membership.GetUser(model.Username, false);
                    if (user != null)
                    {
                        SerializeUser userModel = new SerializeUser
                        {
                            UserId = user.UserId,
                            Nume = user.Nume,
                            Prenume= user.Prenume,
                            DenumireRol = user.Rol.Denumire
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                        (
                            1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(15), model.RememberMe, userData
                        );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(AuthConstants.UserAuthCookie, enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username-ul sau parola sunt gresite!");
            return View(model);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie(AuthConstants.UserAuthCookie, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}