using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class OldBook
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public int ClassCode { get; set; }
        public int Copy { get; set; }
        public int InventoryColumn { get; set; }
        public int InventoryRow { get; set; }
        [Required]
        [StringLength(400)]
        public string Title { get; set; }
        public string Authors { get; set; }
        [Required]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
        public short ReleaseYear { get; set; }
        [Required]
        public DateTime? DateAdded { get; set; }
        public int NumberAvailable { get; set; }
        // genres that are set to this book
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Price { get; set; }
        public string Source { get; set; }
    }
}