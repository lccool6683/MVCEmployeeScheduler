using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public partial class ServiceType
    {
        public Guid ID { get; set; }
        public string BranchName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Description { get; set; }
        public DateTime MinutesPerUnit { get; set; }
        public decimal CostPerUnit { get; set; }
        public string Currency { get; set; }
    }
}