using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace LMS.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int InventoryColumn { get; set; }
        public int InventoryRow { get; set; }
        public bool Rented { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}