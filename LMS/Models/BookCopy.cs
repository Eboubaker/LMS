using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LMS.Models;

namespace LMS.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public bool Rented { get; set; }
        [NotMapped]
        public Customer Customer { get; set; }
    }
}