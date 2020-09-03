using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals/New
        public ActionResult New()
        {
            return View();
        }

        // GET: Rentals/ForCustomer/{id}
        public ActionResult ForCustomer(int id)
        {
            return View();
        }
        // GET: Rentals/ForBook/{id}
        public ActionResult ForBook(int id)
        {
            return View();
        }
        //List<BookCopy> bookCopies = new List<BookCopy>();
        //if (book.NumberInStock - book.NumberAvailable > 0)
        //{
        //    var rentedCopies = _context.Rentals.Where(m => m.BookId == book.Id).Select(m => m.BookCopy).ToList();
        //    bookCopies.AddRange(rentedCopies);
        //}
        //var viewModel = new BookDetailsViewModel()
        //{
        //    Book = book,
        //    RentedBooks = bookCopies
        //};
    }
}