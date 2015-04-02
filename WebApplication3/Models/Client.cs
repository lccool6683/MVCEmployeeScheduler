using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCEmployeeScheduler.Models
{
    public class Client
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string BillingAddress { get; set; }

    }
}