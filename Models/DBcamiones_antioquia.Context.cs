﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace camiones_antioquia.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class camiones_antioquiaEntities : DbContext
    {
        public camiones_antioquiaEntities()
            : base("name=camiones_antioquiaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Camion> Camions { get; set; }
        public virtual DbSet<FotoPesaje> FotoPesajes { get; set; }
        public virtual DbSet<Pesaje> Pesajes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
