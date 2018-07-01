using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicentaApp.Domain.Auth.DTO
{
    public class SerializeUser
    {
        public int UserId { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string DenumireRol { get; set; }
        public int IdMagazin { get; set; }
    }
}