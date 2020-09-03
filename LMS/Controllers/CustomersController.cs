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

namespace LMS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customers/Details/{id}
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
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
            var customerForEdit = new CustomerFormViewModel()
            {
                Customer = customer,
            };
            return View(customerForEdit);
        }

        // POST: Customers/Add
        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerForEdit = new CustomerFormViewModel()
                {
                    Customer = customer,
                };
                return View(customerForEdit);
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
                var customerForEdit = new CustomerFormViewModel()
                {
                    Customer = customer,
                };
                return View(customerForEdit);
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
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound("Customer Not Found");
            }
            _context.Customers.Remove(customer);
            if (_context.SaveChanges() > 0)
            {
                return Json(new { success = true});
            }
            return HttpNotFound("Failed to Delete Customer");
        }
        #region DataTables
        // Post Customers/Table/{request}
        public ActionResult Table(IDataTablesRequest request)
        {
            var filteredData = Filter(request);// Process Sorting, Searching & Paging
            var response = DataTablesResponse.Create(request, filteredData.Count(), _context.Customers.Count(), filteredData);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        private List<Customer> Filter(IDataTablesRequest request)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Customers;
            var filteredData = data.AsQueryable();

            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => m.Name.Contains(request.Search.Value));
            }
            if (columns[0].Sort != null)// Title
            {
                if (columns[0].Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Name);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Name);
            }
            else if (columns[2].Sort != null)//Stock
            {
                if (columns[2].Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Birthdate);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Birthdate);
            }
            else
            {
                filteredData = filteredData.OrderByDescending(m => m.Name);
            }
            return filteredData.Skip(request.Start).Take(request.Length).ToList();
        }
        #endregion
    }
}