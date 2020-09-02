using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.ViewModels;
using LMS.Models;

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
            var customers = _context.Customers.Include(nameof(Customer.MembershipType)).ToList();
            var c = 2;
            return View(customers);
        }
        // GET: Customers/Details/{id}
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(nameof(Customer.MembershipType)).SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        // GET: Customers/New
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewmodel = new CustomerFormViewModel() { MembershipTypes = membershipTypes };

            return View(viewmodel);
        }
        // POST: Customers/Add
        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Customers");
        }
        // POST: Customers/Update
        [HttpPost]
        public ActionResult Update(Customer customer)
        {
            var customerInDb = _context.Customers.SingleOrDefault(m => m.Id == customer.Id);
            if (customerInDb == null)
            {
                return Content("&Dagger;");// nice try
            }
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscripedToNewsLetter = customer.IsSubscripedToNewsLetter;
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        // Get: Customers/Edit
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                var customerForEdit = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View(customerForEdit);
            }
        }
    }
}