﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataServices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<user_roles> user_roles { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<gender> genders { get; set; }
        public virtual DbSet<tax_types> tax_types { get; set; }
        public virtual DbSet<therapist_types> therapist_types { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<therapist_status> therapist_status { get; set; }
        public virtual DbSet<therapist> therapists { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<therapist_requirements> therapist_requirements { get; set; }
        public virtual DbSet<therapist_requirements_types> therapist_requirements_types { get; set; }
    }
}
