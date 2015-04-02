using System;
using System.Data.Entity;

namespace MVCEmployeeScheduler.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
        public Guid ManagerID { get; set; }
        public Guid ScheduleID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string StaffType { get; set; }
        public string EmailAddress { get; set; }
        public string BranchName { get; set; }
        public string Qualification { get; set; }
        public string EmergencyContact { get; set; }
        public string ContactInformation { get; set; }
    }
}