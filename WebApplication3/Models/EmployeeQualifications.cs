using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public class EmployeeQualifications
    {
        [Key]
        public int QualificationsID { get; set; }
        public string EmployeeID { get; set; }
        public string Qualifications { get; set; }
    }
}