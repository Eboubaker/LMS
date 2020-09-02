using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }
    }
}