using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.ViewModels;
using LMS.Models;
using AutoMapper;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System.Data.Entity;
namespace LMS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly LibraryManagmentContext _context;

        public CustomersController()
        {
            _context = new LibraryManagmentContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        #region Pages
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customers/Details/{id}
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(m => m.Rentals).SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        // GET: Customers/New
        public ActionResult New()
        {
            return View();
        }

        // Get: Customers/Edit
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound("Customer Not Found");
            }
            return View(customer);
        }
        #endregion

        #region Modifiers
        // POST: Customers/Add
        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("New", customer);
            }
            customer = _context.Customers.Add(customer);
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = customer.Id});
            }
            return HttpNotFound("Failed to Add Customer");
        }
        // POST: Customers/Update
        [HttpPost]
        public ActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("edit", customer);
            }
            var customerInDb = _context.Customers.SingleOrDefault(m => m.Id == customer.Id);
            if (customerInDb == null)
            {
                return HttpNotFound("Customer Not Found");
            }
            Mapper.Map(customer, customerInDb);
            if(_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = customerInDb.Id});
            }
            return HttpNotFound("Failed to Save Customer");
        }
        // POST: Customers/Delete/id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Include(m => m.Rentals).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return Json(new { success = false , error="Customer #"+id+" not found"});
            }
            if(customer.Rentals.Count > 0)
            {
                return Json(new { success = false, error = "Put Back all rentals first, <a style='color:yellow !important;' href='/rentals/forcustomer/"+id+"'>View</a>" });
            }
            _context.Customers.Remove(customer);
            if (_context.SaveChanges() > 0)
            {
                return Json(new { success = true, message="Customer #" + id + " was removed"});
            }
            return Json(new { success = false, error = "Failed to delete customer #" + id});
        }
        #endregion

        #region Misc
        public ActionResult Choose(int? bookId, int? copyId)
        {
            var model = new NewRentalViewModel();
            if (bookId.HasValue)
                model.BookId = bookId.Value;
            if (copyId.HasValue)
            {
                model.CopyId = copyId.Value;
            }
            return View("Index", model);
        }
        #endregion

        #region DataTables
        // Post Customers/Table/{request}
        [HttpPost]
        public ActionResult Table(IDataTablesRequest request)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Customers;
            var filteredData = data.Include(m => m.Rentals);
            int count = data.Count();
            var name = columns.Find(m => m.Name == "Name");
            var rented = columns.Find(m => m.Name == "RentalsCount");
            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => m.Name.Contains(request.Search.Value));
                count = filteredData.Count();
            }
            if (name.Sort != null)
            {
                if (name.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Name);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Name);
            }
            else if (rented.Sort != null)
            {
                if (rented.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Rentals.Count);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Rentals.Count);
            }
            filteredData = filteredData.Skip(request.Start);
            if (request.Length > 0)
            {
                filteredData = filteredData.Take(request.Length);
            }
            var filteredList = filteredData.ToList();
            filteredList.ForEach(m => {
                m.Rentals = Enumerable.Repeat(new Rental(), m.Rentals.Count).ToList();
            });
            var response = DataTablesResponse.Create(request, filteredList.Count(), count, filteredList);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}