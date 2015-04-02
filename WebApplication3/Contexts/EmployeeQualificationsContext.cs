using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
using System.Linq;
using System.Web;
using MVCEmployeeScheduler.Models;

namespace MVCEmployeeScheduler.Contexts
{
    public class EmployeeQualificationsContext : DbContext
    {
        public EmployeeQualificationsContext() : base("DefaultConnection") { }

        public DbSet<EmployeeQualifications> EmployeeQualifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        internal void Refresh(System.Data.Linq.RefreshMode refreshMode, DbSet<EmployeeQualifications> dbSet)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges(bool refreshOnConcurrencyException, RefreshMode refreshMode = RefreshMode.KeepChanges)
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (DbEntityEntry entry in ex.Entries)
                {
                    if (refreshMode == RefreshMode.KeepChanges)
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    else
                        entry.Reload();
                }
                return SaveChanges();
            }
        }
    }
}