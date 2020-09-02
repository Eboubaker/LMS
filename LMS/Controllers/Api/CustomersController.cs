using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS.Models;
using System.Data.Entity;

namespace LMS.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            //var customersQuery = 
            //if (!String.IsNullOrWhiteSpace(query))
            //    customersQuery = _context.Customers.customersQuery.Where(c => c.Name.Contains(query));
            //var customerDtos = _context.Customers
            //                .ToList()
            //                .Select(Mapper.Map<Customer, Customer>);
            return Ok();
        }
        // GET /api/customers/{id}
        public IHttpActionResult GetCustomer(int id)
        {
            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, Customer>(customer));
        }
        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<Customer, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        // Put /api/customers
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, Customer customerDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                NotFound();

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok(customerDto);
        }
        // DELETE /api/customers
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            var customerDto = Mapper.Map<Customer, Customer>(customerInDb);
            return Ok(customerDto);
        }
    }
}
