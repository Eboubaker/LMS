using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models;
namespace LMS.ViewModels
{
    public class BookFormViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Class> Classes { get; set; }
    }
}