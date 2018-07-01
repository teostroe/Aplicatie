using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LicentaApp.Domain.Auth
{
    public class AppMembershipUser : MembershipUser
    {
        #region User Properties  

        public int UserId { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public Roluri Rol { get; set; }
        public int IdMagazin { get; set; }
        #endregion

        public AppMembershipUser(Utilizatori user):base("AppMembershipProvider", user.Username, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)  
        {
            UserId = user.Id;
            Nume = user.Nume;
            Prenume = user.Prenume;
            Rol = user.Roluri;
            IdMagazin = user.IdMagazin;
        }
    }
}