using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Contexts
{
    public class EmergencyContactContext : DbContext
    {
        public EmergencyContactContext() : base("DefaultConnection") { }

        public DbSet<EmergencyContact> EmergencyContact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}