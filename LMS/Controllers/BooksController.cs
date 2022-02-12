using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using LMS.ViewModels;
using System.Data.Entity;
using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;
using System.Web.Http.Results;
using Glimpse.Core.Extensions;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

namespace LMS.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryManagmentContext _context;
        public BooksController()
        {
            _context = new LibraryManagmentContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Books/Random
        public ActionResult Random()
        {
            var book = _context.BookCopies.OrderBy(m => Guid.NewGuid()).First();
            if(book == null)
            {
                return RedirectToAction("Index");
            }
            return View("Details",book);
        }
        // GET: Books/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Books/Details/{id}
        public ActionResult Details(int id)
        {
            var book = _context.Books
                .Include(m => m.Class)
                .Include(m => m.Language)
                .Include(m => m.Rentals)
                .Include(m => m.BookCopys)
                .SingleOrDefault(c => c.Id == id);
            if(book == null)
            {
                return Content("ID not Found");
            }
            return View(book);
        }
        // POST: Books/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Book book)
        {
            book.DateAdded = DateTime.Now;
            //if (!ModelState.IsValid)
            //{
            //    return Content("Invalid Model State");
            //}
            _context.Books.Add(book);
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = book.Id});
            }
            return Content("Can't Add Book");
        }
        // Get: Books/Edit/{id}
        public ActionResult Edit(int id)
        {
            var book = _context.Books.Include(m => m.Class).Include(m => m.Language).SingleOrDefault(m => m.Id == id);
            if(book == null)
            {
                return Content("ID Not Found");
            }
            var classes = _context.Classes.ToList();
            var languages = _context.Languages.ToList();
            var viewModel = new BookFormViewModel()
            {
                Book = book,
                Classes = classes,
                Languages = languages
            };
            return View(viewModel);
        }
        // POST: Books/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Book book)
        {
            var bookInDb = _context.Books.SingleOrDefault(m => m.Id == book.Id);
            if (bookInDb == null)
            {
                return Content("ID Not Found");
            }
            Mapper.Map(book, bookInDb);
            //bookInDb.Price = book.Price;
            if(_context.SaveChanges() > 0)
                return RedirectToAction("Index");
            return Content("Can't Save Book");
        }
        // Get: Books/New/
        public ActionResult New()
        {
            var classes = _context.Classes.ToList();
            var languages = _context.Languages.ToList();
            var viewModel = new BookFormViewModel()
            {
                Classes = classes,
                Languages = languages
            };
            return View(viewModel);
        }
        // Post: Books/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return Json(new { success = false, error = "Book #" + id + " not found" }, JsonRequestBehavior.AllowGet);
            _context.Books.Remove(book);
            if (_context.SaveChanges() == 0)
                return Json(new { success = false, message = "can't remove Book #" + id}, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, message = "Book #" + id + "was removed"}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Choose(int? customerId)
        {
            var model = new NewRentalViewModel();
            if (customerId.HasValue)
            {
                model.CustomerId = customerId.Value;
            }
            return View("Index", model);
        }
        // Post Books/Table/{request}
        [HttpPost]
        public ActionResult Table(IDataTablesRequest request)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Books;
            var filteredData = data.Include(m => m.Class).Include(m => m.Rentals).Include(m => m.BookCopys);
            int count = data.Count();
            var title = columns.Find(m => m.Name == "Title");
            var stock = columns.Find(m => m.Name == "NumberInStock");
            var available = columns.Find(m => m.Name == "NumberAvailable");
            var rented = columns.Find(m => m.Name == "RentalsCount");
            var sorted = false;
            IEnumerable<Book> filteredList = new List<Book>();
            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => m.Title.Contains(request.Search.Value));
                count = filteredData.Count();
            }
            if (title.Sort != null)
            {
                if (title.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Title).ThenByDescending(m => m.Popularity);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Title).ThenByDescending(m => m.Popularity);
                sorted = true;
            }
            else if (rented.Sort != null)
            {
                if (rented.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Rentals.Count).ThenByDescending(m => m.Popularity);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Rentals.Count).ThenByDescending(m => m.Popularity);
                sorted = true;
            }
            filteredList = filteredData.AsEnumerable<Book>();
            if (!sorted)
            {
                
                if (stock.Sort != null)
                {
                    if (stock.Sort.Direction == SortDirection.Ascending)
                        filteredList = filteredList.OrderBy(m => m.NumberInStock).ThenByDescending(m => m.Popularity);
                    else
                        filteredList = filteredList.OrderByDescending(m => m.NumberInStock).ThenByDescending(m => m.Popularity);
                }
                else if (available.Sort != null)
                {
                    if (available.Sort.Direction == SortDirection.Ascending)
                        filteredList = filteredList.OrderBy(m => m.NumberAvailable).ThenByDescending(m => m.Popularity);
                    else
                        filteredList = filteredList.OrderByDescending(m => m.NumberAvailable).ThenByDescending(m => m.Popularity);
                }
            }
            filteredList = filteredList.Skip(request.Start);
            if (request.Length > 0)
            {
                filteredList = filteredList.Take(request.Length);
            }
            var fl = filteredList.ToList();
            fl.ForEach(m =>
            {
                m.BookCopys = Enumerable.Repeat(new BookCopy(), m.BookCopys.Count).ToList();
                m.Rentals = Enumerable.Repeat(new Rental(), m.Rentals.Count).ToList();
            });
            filteredList = fl;
            var response = DataTablesResponse.Create(request, filteredList.Count(), count, filteredList);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}