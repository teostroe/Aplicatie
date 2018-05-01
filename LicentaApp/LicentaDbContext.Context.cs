﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LicentaDbContext : DbContext
    {
        public LicentaDbContext()
            : base("name=LicentaDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Comenzi> Comenzi { get; set; }
        public virtual DbSet<DetaliiProdus> DetaliiProdus { get; set; }
        public virtual DbSet<Furnizori> Furnizori { get; set; }
        public virtual DbSet<Inventar> Inventar { get; set; }
        public virtual DbSet<Magazine> Magazine { get; set; }
        public virtual DbSet<Preturi> Preturi { get; set; }
        public virtual DbSet<Produse> Produse { get; set; }
        public virtual DbSet<RandComenziProduse> RandComenziProduse { get; set; }
        public virtual DbSet<Roluri> Roluri { get; set; }
        public virtual DbSet<Utilizatori> Utilizatori { get; set; }
        public virtual DbSet<ViziteMedicale> ViziteMedicale { get; set; }
    
        public virtual int P_LOGIN(string x, string y, ObjectParameter z)
        {
            var xParameter = x != null ?
                new ObjectParameter("X", x) :
                new ObjectParameter("X", typeof(string));
    
            var yParameter = y != null ?
                new ObjectParameter("Y", y) :
                new ObjectParameter("Y", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("P_LOGIN", xParameter, yParameter, z);
        }
    }
}
