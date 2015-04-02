using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public class Events
    {
        public int ID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid ServiceID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public Boolean Repeated { get; set; }
        public string RepeatedOption { get; set; }
        public int RepeatedEvery { get; set; }
        public int RepeatedTimes { get; set; }

        public Boolean RepeatedMonday { get; set; }
        public Boolean RepeatedTuesday { get; set; }
        public Boolean RepeatedWednesday { get; set; }
        public Boolean RepeatedThursday { get; set; }
        public Boolean RepeatedFriday { get; set; }
        public Boolean RepeatedSaturday { get; set; }
        public Boolean RepeatedSunday { get; set; }
        public string Status { get; set; }
        public int ActualDuration { get; set; }
        [DataType(DataType.Date)]
        public DateTime CompleteDate { get; set; }
    }
}