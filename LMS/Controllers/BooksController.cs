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

namespace LMS.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        ApplicationDbContext _context;
        public BooksController()
        {
            _context = new ApplicationDbContext();
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
            return View(User);
        }

        // GET: Books/Details/{id}
        public ActionResult Details(int id)
        {
            var book = _context.Books
                .Include(m => m.Class)
                .Include(m => m.Language)
                .SingleOrDefault(c => c.Id == id);
            if(book == null)
            {
                return HttpNotFound("ID not Found");
            }
            return View(book);
        }
        // POST: Books/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Book book)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound("Invalid Model State");
            }
            _context.Books.Add(book);
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = book.Id});
            }
            return HttpNotFound("Can't Add Book");
        }
        // Get: Books/Edit/{id}
        public ActionResult Edit(int id)
        {
            var book = _context.Books.Include(m => m.Class).Include(m => m.Language).SingleOrDefault(m => m.Id == id);
            if(book == null)
            {
                return HttpNotFound("ID Not Found");
            }
            var classes = _context.Classes.ToList();
            var languages = _context.Languages.ToList();
            var viewModel = new BookFormViewModel()
            {
                Book = book,
                Classes = classes,
                Languages = languages
            };
            return View("Form", viewModel);
        }
        // POST: Books/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Book book)
        {
            var bookInDb = _context.Books.SingleOrDefault(m => m.Id == book.Id);
            if (bookInDb == null)
            {
                return HttpNotFound("ID Not Found");
            }
            Mapper.Map(book, bookInDb);
            //bookInDb.Price = book.Price;
            if(_context.SaveChanges() > 0)
                return RedirectToAction("Index");
            return HttpNotFound("Can't Save Book");
        }
        // Get: Books/New/
        public ActionResult New()
        {
            var classes = _context.Classes.ToList();
            var languages = _context.Languages.ToList();
            var viewModel = new BookFormViewModel()
            {
                Book = new Book() { Class = new Class() { Name = "" }, Language = new Language() { Name = "" } },
                Classes = classes,
                Languages = languages
            };
            return View("Form", viewModel);
        }
        // Get: Books/Delete/{id}
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return HttpNotFound("ID Not Found");
            _context.Books.Remove(book);
            if (_context.SaveChanges() > 0)
                return Json(new { success = true, action = "Deleted" });
            return HttpNotFound("Can't Delete Book");
        }

        public ActionResult Table(IDataTablesRequest request)
        {
            var filteredData = Filter(request);// Process Sorting, Searching & Paging
            var response = DataTablesResponse.Create(request, filteredData.Count(), _context.Books.Count(), filteredData);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        private List<Book> Filter(IDataTablesRequest request)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Books;
            var filteredData = data.Include(m => m.Class).Include(m => m.Language);

            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => m.Title.Contains(request.Search.Value));
            }
            if (columns[0].Sort != null)// Title
            {
                if (columns[0].Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Title);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Title);
            }
            else if (columns[2].Sort != null)//Stock
            {
                if (columns[2].Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.NumberInStock);
                else
                    filteredData = filteredData.OrderByDescending(m => m.NumberInStock);
            }
            else if (columns[3].Sort != null)//Available
            {
                if (columns[3].Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.NumberAvailable);
                else
                    filteredData = filteredData.OrderByDescending(m => m.NumberAvailable);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(m => m.Popularity);
            }
            return filteredData.Skip(request.Start).Take(request.Length).ToList();
        }
    }
}