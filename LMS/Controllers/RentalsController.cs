using LMS.Models;
using LMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System.Globalization;
using System.Threading;
using System.Web.DynamicData;

namespace LMS.Controllers
{
    public class RentalsController : Controller
    {
        private readonly LibraryManagmentContext _context;
        public RentalsController()
        {
            _context = new LibraryManagmentContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View(new NewRentalViewModel());
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var rental = _context.Rentals.Include(m => m.Customer).Include(m => m.Book).Include(m => m.BookCopy).FirstOrDefault(m => m.Id == id);
            if (rental == null)
            {
                return Json(new{ success=false, error="can't find rental #" + id}, JsonRequestBehavior.AllowGet);
            }
            return View(rental);
        }
        public ActionResult New(int? customerId, int? copyId)
        {
            var model = new NewRentalViewModel();
            if (customerId.HasValue)
            {
                model.CustomerId = customerId;
                model.Customer = _context.Customers.FirstOrDefault(m => m.Id == customerId.Value);
            }
            if (copyId.HasValue)
            {
                model.CopyId = copyId;
                model.Copy = _context.BookCopies.Include(m => m.Book).Include(m => m.Inventory).FirstOrDefault(m => m.Id == copyId);
            }
            return View(model);
        }
        public ActionResult Add(int customerId, int copyId, int duration)
        {
            if (duration <= 0)
            {
                return Content("Error: rent duration must be greater than 1 day");
            }
            var copy = _context.BookCopies.FirstOrDefault(m => m.Id == copyId);
            var customer = _context.Customers.FirstOrDefault(m => m.Id == customerId);
            if(copy == null)
            {
                return Content("Error: can't find book copy #" + copyId);
            }
            if (customer == null)
            {
                return Content("Error: can't find customer #" + customerId);
            }
            if (copy.Rented)
            {
                return Json(new { success = false, error = "This copy is already rented to customer #" 
                    + _context.Rentals.FirstOrDefault(m => m.BookCopyId == copy.Id).CustomerId }
                , JsonRequestBehavior.AllowGet);
            }
            var rental = new Rental()
            {
                BookCopyId = copy.Id,
                CustomerId = customer.Id,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(duration),
                BookId = copy.BookId
            };
            _context.Rentals.Add(rental);
            copy.Rented = true;
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = rental.Id });
            }
            return Content("Error: Can't add new rental");
        }
        // TODO:
        public ActionResult ForCustomer(int id)
        {
            var rental = _context.Rentals.Include(m => m.Customer).FirstOrDefault(m => m.CustomerId == id);
            if (rental == null)
            {
                return Json(new { success = false, error = "can't find customer #" + id }, JsonRequestBehavior.AllowGet);
            }
            var model = new NewRentalViewModel() { Customer = rental.Customer};
            return View("Index", model);
        }
        // TODO:
        public ActionResult ForBook(int id)
        {
            var book = _context.Books.FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return Json(new { success = false, error = "can't find book #" + id }, JsonRequestBehavior.AllowGet);
            }
            var model = new NewRentalViewModel() { Book = book };
            return View("Index", model);
        }
        // TODO:
        public ActionResult ForCopy(int id)
        {
            var rental = _context.Rentals.Include(m => m.Customer).Include(m => m.Book).Include(m => m.BookCopy).FirstOrDefault(m => m.BookCopyId == id);
            if (rental == null)
            {
                return Json(new { success = false, error = "can't find book copy #" + id }, JsonRequestBehavior.AllowGet);
            }
            return View("Details", rental);
        }
        // TODO:
        public ActionResult Return(int id)
        {
            var rental = _context.Rentals.Include(m => m.BookCopy).FirstOrDefault(m => m.Id == id);
            if(rental == null)
            {
                return Json(new { success = false, error = "can't find rental #" + id }, JsonRequestBehavior.AllowGet);
            }
            rental.BookCopy.Rented = false;
            _context.Rentals.Remove(rental);
            if (_context.SaveChanges() > 0) {
                return Json(new { success = true, message = "Book copy # " + rental.BookCopyId + " restored" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, error = "can't delete rental #" + id }, JsonRequestBehavior.AllowGet);
        }


        // Post Books/Table/{request}
        [HttpPost]
        public ActionResult Table(IDataTablesRequest request, int bookId, int customerId)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Rentals;
            int count = data.Count();
            var filteredData = data.Include(m => m.BookCopy.Book).Include(m => m.Customer);
            if (bookId != -1)
            {
                filteredData = filteredData.Where(m => m.BookId == bookId);
                count = filteredData.Count();
            }
            else if (customerId != -1)
            {
                filteredData = filteredData.Where(m => m.CustomerId == customerId);
                count = filteredData.Count();
            }
            var title = columns.Find(m => m.Name == "Title");
            var card = columns.Find(m => m.Name == "CardId");
            var created = columns.Find(m => m.Name == "Created");
            var remaining = columns.Find(m => m.Name == "RemainingDays");

            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => m.Book.Title.Contains(request.Search.Value) || m.Customer.CardId == request.Search.Value);
                count = filteredData.Count();
            }
            if (title.Sort != null)
            {
                if (title.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Book.Title).ThenBy(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
                else
                    filteredData = filteredData.OrderByDescending(m => m.Book.Title).ThenBy(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
            }
            else if (created.Sort != null)
            {
                if (created.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Created).ThenBy(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
                else
                    filteredData = filteredData.OrderByDescending(m => m.Created).ThenBy(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
            }
            else if (remaining.Sort != null)
            {
                if (remaining.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
                else
                    filteredData = filteredData.OrderByDescending(m => DbFunctions.DiffDays(m.Expires, DateTime.Now));
            }
            filteredData = filteredData.Skip(request.Start);
            if (request.Length > 0)
            {
                filteredData = filteredData.Take(request.Length);
            }
            var filteredList = filteredData.ToList();
            filteredList.ForEach(m => {
                // to prevent Json loop Exception
                m.Customer.Rentals = null;
                m.BookCopy.Inventory = null;
                m.BookCopy.Book.Rentals = null;
                m.BookCopy.Book.BookCopys = null;
            });
            var response = DataTablesResponse.Create(request, filteredList.Count(), count, filteredList);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}