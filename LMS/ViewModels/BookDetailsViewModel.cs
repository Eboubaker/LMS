using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.ViewModels
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public ICollection<BookCopy> RentedBooks { get; set; }
    }
}