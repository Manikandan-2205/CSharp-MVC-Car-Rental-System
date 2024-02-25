using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftRent.Models
{
    public class RentalReportViewModel
    {
        // Define properties as needed
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ExpiredDays { get; set; }
        public int Fee { get; set; }
        public int Fine { get; set; }
    }
}