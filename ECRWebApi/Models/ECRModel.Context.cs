﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECRWebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CalECREntities : DbContext
    {
        public CalECREntities()
            : base("name=CalECREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeEmail> EmployeeEmail { get; set; }
        public virtual DbSet<EmployeePhone> EmployeePhone { get; set; }
        public virtual DbSet<vEmployeeInfo> vEmployeeInfo { get; set; }
    }
}
