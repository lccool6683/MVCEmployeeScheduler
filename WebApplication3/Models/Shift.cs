using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public class Shift
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string BranchName { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
    }
}