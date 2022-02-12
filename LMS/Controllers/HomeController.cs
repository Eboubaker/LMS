using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        LibraryManagmentContext _context;

        public HomeController()
        {
            _context = new LibraryManagmentContext();
        }
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            model.BookCount = _context.Books.Count();
            model.BookCopiesCount = _context.BookCopies.Count();
            model.ActiveRentalsCount = _context.Rentals.Count();
            model.ExpiredRentalsCount = _context.Rentals.Where(m => DbFunctions.DiffDays(m.Expires, m.Created) < 0).Count();
            model.CustomersCount = _context.Customers.Count();
            model.MaxRentedCustomer = _context.Customers.Include(m => m.Rentals).OrderByDescending(m => m.Rentals.Count).First().Rentals.Count;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}