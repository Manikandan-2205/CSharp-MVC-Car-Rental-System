using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftRent.Models
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        public string CarId { get; set; }
        public Nullable<int> CustId { get; set; }
        public string CustName { get; set; }
        public Nullable<int> Fee { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string Available { get; set; }
    }
}