using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Contexts
{
    public class EmployeeContactsContext : DbContext
    {
        public EmployeeContactsContext() : base("DefaultConnection") { }

        public DbSet<EmployeeContacts> EmployeeContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}