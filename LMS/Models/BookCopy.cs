using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BaseBookId { get; set; }
        public Book BaseBook { get; set; }
        public int InventoryColumn { get; set; }
        public int InventoryRow { get; set; }
        public bool Rented { get; set; }
    }
}