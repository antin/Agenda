﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agenda.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AgendaEntities : DbContext
    {
        public AgendaEntities()
            : base("name=AgendaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AGENDA> AGENDA { get; set; }
        public virtual DbSet<MK> MK { get; set; }
        public virtual DbSet<MK_SCORE> MK_SCORE { get; set; }
        public virtual DbSet<MK_TO_PARTY> MK_TO_PARTY { get; set; }
        public virtual DbSet<PARTY> PARTY { get; set; }
        public virtual DbSet<BILL> BILL { get; set; }
        public virtual DbSet<VOTE> VOTE { get; set; }
        public virtual DbSet<VOTE_TO_AGENDA> VOTE_TO_AGENDA { get; set; }
        public virtual DbSet<ITEM2AGENDA> ITEM2AGENDA { get; set; }
        public virtual DbSet<Table> Table { get; set; }
    }
}
