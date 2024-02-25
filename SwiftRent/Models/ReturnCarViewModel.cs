using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftRent.Models
{
    public class ReturnCarViewModel
    {
        public int Id { get; set; }
        public string CarNo { get; set; }
        public int CustId { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public int? Elsp { get; set; }
        public int? Fine { get; set; }
        public string Available { get; set; }
    }
}