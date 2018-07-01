using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LicentaApp.Domain.Auth
{
    public class AppPrincipal : IPrincipal
    {
        #region Identity Properties  

        public int UserId { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Rol { get; set; }
        public int IdMagazin { get; set; }
        #endregion

        public AppPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            if (this.Rol == role)
            {
                return true;
            }
            return false;
        }

        public IIdentity Identity { get; }
    }
}