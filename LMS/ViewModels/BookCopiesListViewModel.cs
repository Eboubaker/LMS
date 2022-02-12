using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.ViewModels
{
    public class BookCopiesViewModel
    {
        public NewRentalViewModel RentalForm { get; set; }
        public Book Book { get; set; }
        public Inventory Inventory { get; set; }

    }
}