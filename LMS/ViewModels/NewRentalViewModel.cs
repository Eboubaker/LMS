using LMS.Models;
using System;


namespace LMS.ViewModels
{
    public class NewRentalViewModel
    {
        public int? CustomerId { get; set; }
        public int? BookId { get; set; }
        public int? CopyId { get; set; }

        public BookCopy Copy { get; set; }
        public Book Book { get; set; }
        public Customer Customer { get; set; }
    }
}