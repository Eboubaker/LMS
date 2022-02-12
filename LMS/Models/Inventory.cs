using LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int Shelf { get; set; }
        public int Size { get; set; }
        public ICollection<BookCopy> BookCopys { get; set; }
    }
}