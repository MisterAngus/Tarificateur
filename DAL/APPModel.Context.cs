﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APP_DBEntities : DbContext
    {
        public APP_DBEntities()
            : base("name=APP_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Professions> Professions { get; set; }
        public virtual DbSet<Professions_Lgn_Tarifs> Professions_Lgn_Tarifs { get; set; }
        public virtual DbSet<Statuts_Tns> Statuts_Tns { get; set; }
    }
}