using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCEmployeeScheduler.Models
{
    public class Service
    {
        public Guid ID { get; set; }
        public Guid ServiceTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ClientID { get; set; }
        public string RepeatOption { get; set;}
        public DateTime RepeatUntil { get; set; }
        public Guid ShiftID { get; set; }

    }
}