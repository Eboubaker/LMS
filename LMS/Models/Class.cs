using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Class
    {
        public int Id { get; set; }
        [StringLength(6)]
        public string CodeName { get; set; }
        public string Name { get; set; }
    }
}