using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCEmployeeScheduler.Models
{
    public class OffDays
    {
        //public int ID { get; set; }
        //public DateTime DateTimeFrom { get; set; }
        //public DateTime DateTimeTo { get; set; }
        //public string Description { get; set; }
        //public Nullable<int> EmpId { get; set; }
        //public string DateFrom { get; set; }
        //public string DateTo { get; set; }
        //public string TimeFrom { get; set; }
        //public string TimeTo { get; set; }

        public int ID { get; set; }
        public string Description { get; set; }
        public string BranchName { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }

    }
}