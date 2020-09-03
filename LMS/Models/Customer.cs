using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is Required")]
        [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Name Should be 3 to 255 Long")]
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        [Index(IsUnique = true)]
        [Display(Name = "Card Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Card Id is Required")]
        [StringLength(maximumLength: 25,MinimumLength = 4)]
        public string CardId { get; set; }
        public int RentalsCount { get; set; }
    }
}