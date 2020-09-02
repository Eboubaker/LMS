using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookCopyId { get; set; }
        public int BookId { get; set; }
        public DateTime Created { get; set; }


        public Customer Customer { get; set; }
        public BookCopy BookCopy { get; set; }
        public Book Book { get; set; }
    }
}