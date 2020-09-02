using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS.Models;

namespace LMS.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // POST /api/CreateNewRentals/{rentaldto}
        [HttpPost]
        public IHttpActionResult CreateNewRentals(Rental newRental)
        {
            //if (newRental == null)
            //    return BadRequest("bad input");
            //if (newRental.CustomerId == 0)
            //    return BadRequest("CustomerId is invalid");
            //if (newRental.BookIds.Count == 0)
            //    return BadRequest("No Movies have been given");
            //var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            //if (customer == null)
            //    return BadRequest("Customer Not Found");
            //var oldCount = newRental.BookIds.Count;
            //newRental.BookIds = new HashSet<int>(newRental.BookIds).ToList();
            //if(newRental.BookIds.Count != oldCount)
            //    return BadRequest("There are one or more dublicated Movies");
            //var movies = _context.BookCopies.Where(c => newRental.BookIds.Contains(c.Id)).ToList();
            //if (movies.Count() != newRental.BookIds.Count)
            //    return BadRequest("a movie in the movies list was not found");

            //foreach(var movie in movies)
            //{
            //    if (movie.BaseBook.NumberAvailable == 0)
            //        return BadRequest("Movie " + movie.Id + " is Not Available");
            //}
            //// now we are safe from bad input
            //foreach (var movie in movies)
            //{
            //    //var rental = new Rental()
            //    //{
            //    //    Customer = customer,
            //    //    //Movie = movie,
            //    //    DateRented = DateTime.Now
            //    //};
            //    //_context.Rentals.Add(rental);
            //    //movie.BaseBook.NumberAvailable--;
            //}
            //_context.SaveChanges();
            return Ok();
        }
    }
}