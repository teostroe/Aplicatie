//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LicentaApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Utilizatori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilizatori()
        {
            this.Comenzi = new HashSet<Comenzi>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
        public int IdMagazin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comenzi> Comenzi { get; set; }
        public virtual Magazine Magazine { get; set; }
        public virtual Roluri Roluri { get; set; }
    }
}
