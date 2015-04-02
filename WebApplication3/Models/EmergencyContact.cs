using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public class EmergencyContact
    {
        [Key]
        public int ContactID { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactCity { get; set; }
        public string ContactAddress { get; set; }
    }
}
