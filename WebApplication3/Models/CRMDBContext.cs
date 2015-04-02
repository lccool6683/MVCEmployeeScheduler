using MVCEmployeeScheduler.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Entity.Infrastructure;

namespace MVCEmployeeScheduler.Models
{
    public class CRMDBContext : DbContext
    {
        //Ming's offday calendar
        public DbSet<OffDays> OffDaysCalendar { get; set; }

        public DbSet<Shift> Shift { get; set; }

        public DbSet<Branches> Branches { get; set; }
        
        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<EmployeeQualifications> EmployeeQualifications { get; set; }

        public DbSet<EmergencyContact> EmergencyContact { get; set; }

        public DbSet<EmployeeContacts> EmployeeContacts { get; set; }

        public virtual DbSet<Schedules> ScheduleModels { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Service> Services { get; set; }

        //Stefan's offday scheduler
        public virtual DbSet<OffDay> OffDaysSchedule { get; set; }

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