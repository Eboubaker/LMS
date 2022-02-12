﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int ClassCode { get; set; }
        [Required]
        [StringLength(maximumLength:400 ,MinimumLength = 3, ErrorMessage = "Title must be 3-400 Long")]
        public string Title { get; set; }
        public string Authors { get; set; }
        [Required]
        [Display(Name = "Year Released")]
        public short ReleaseYear { get; set; }
        [Required]
        public DateTime? DateAdded { get; set; }
        
        // genres that are set to this book
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Price { get; set; }
        public string Source { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public float Popularity { get; set; }
        
        public List<BookCopy> BookCopys { get; set; }
        public List<Rental> Rentals { get; set; }
        
        [NotMapped]
        public int NumberInStock => BookCopys != null ? BookCopys.Count : -1;
        [NotMapped]
        public int NumberAvailable => BookCopys != null && Rentals != null ? BookCopys.Count - Rentals.Count : -1;
        [NotMapped]
        public int RentalsCount => Rentals != null ? Rentals.Count : -1;
    }
}