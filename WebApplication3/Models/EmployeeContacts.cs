using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public class EmployeeContacts
    {
        [Key]
        public string EmployeeID    { get; set; }
        public string Name          { get; set; }
        public string PhoneNumber   { get; set; }
        public string City          { get; set; }
        public string PostalCode    { get; set; }
        public string StreetAddress { get; set; }
    }
}