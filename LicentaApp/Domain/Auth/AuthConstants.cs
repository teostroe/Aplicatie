using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Auth
{
    public class AuthConstants
    {
        public const string UserAuthCookie = "UserAuthCookie";

        public class Roluri
        {
            public const string Admin = "Administrator";
            public const string Utilizator = "Utilizator";
        }

        public class Permisii
        {
            public const string AdminOnly = Roluri.Admin;
            public const string UtilizatorOnly = Roluri.Utilizator;
            public const string AdminUtilizator = Roluri.Admin + ", "+ Roluri.Utilizator;
        }

    }
}