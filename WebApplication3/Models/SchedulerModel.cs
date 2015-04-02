using System;

namespace MVCEmployeeScheduler.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory

        public int taskid { get; set; }
        public int staffid { get; set; }
        public int clientid { get; set; }        
        public string text { get; set; }
        public string location { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}