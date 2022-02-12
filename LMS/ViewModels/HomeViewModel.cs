using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.ViewModels
{
    public class HomeViewModel
    {
        public int BookCount { get; set; }
        public int BookCopiesCount { get; set; }
        public int ActiveRentalsCount { get; set; }
        public int ExpiredRentalsCount { get; set; }
        public int TotalRentals { get; set; }
        public int CustomersCount { get; set; }
        public int MaxRentedCustomer { get; set; }
    }
}