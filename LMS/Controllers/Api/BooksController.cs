using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS.Models;
using System.Data.Entity;
using Vidly.Models;
using Microsoft.Ajax.Utilities;
using Glimpse.Mvc.AlternateType;
using System.Web.Http.Results;
using DataTables.AspNet.Mvc5;
using System.Web.Mvc;
using DataTables.AspNet.Core;

namespace LMS.Controllers.Api
{
    public class BooksController : ApiController
    {
        public ApplicationDbContext _context { get; set; }
        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/books
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Mvc.Bind()]
        public IHttpActionResult GetBooks(DataTableParameters parameters, string query = null)
        {
            if (query != null)
            {
                //this is a query request from dataTables
                return Ok();

            }
            else if (!String.IsNullOrWhiteSpace(query))
            {
                // this is a book search query
                var books = _context.Books
                    .Where(m => m.Title.Contains(query))
                    .OrderByDescending(m => m.Popularity)
                    .Take(10)
                    .Select(m => new { m.Id, m.Title })
                    .ToList();
                return Ok(books);
            }
            else
            {
                // this is a request for all properties
                // return 100 random book list
                var books = _context.Books
                            .OrderBy(_ => Guid.NewGuid())  // randomize
                            .Include(m => m.Class)
                            .Include(m => m.Language)
                            .Take(100)
                            .OrderByDescending(m => m.Popularity)
                            .ToList();
                return Ok(books);
            }  
        }

        // GET /api/books/{id}
        [System.Web.Http.AllowAnonymous]
        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.Include(m => m.Class).Include(m => m.Language).SingleOrDefault(c => c.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        // POST /api/books/
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");
            if(!User.IsInRole(Role.CanAddBooks))
                return BadRequest("Not Authorized");
            book.DateAdded = DateTime.Now;
            book.NumberAvailable = book.NumberInStock;
            _context.Books.Add(book);
            _context.SaveChanges();
            var createdUri = new Uri(Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/books/details" + book.Id);
            return Created(createdUri, book);
        }

        // Put /api/books/{id}
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdatBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");
            if (!User.IsInRole(Role.CanEditBooks))
            {
                return BadRequest("Not Authorized");
            }
            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == book.Id);
            if(bookInDb == null)
            {
                return BadRequest("Not Found");
            }
            // copy past all attributes
            Mapper.Map(book, bookInDb);
            _context.SaveChanges();
            return Ok(bookInDb);
        }

        // DELETE /api/books/{id}
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);
            if (bookInDb == null)
                return NotFound();
            _context.Books.Remove(bookInDb);
            _context.SaveChanges();
            return Ok(bookInDb);
        }
        [System.Web.Http.HttpGet]
        public JsonResult<IDataTablesResponse> Table(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = _context.Books;

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item => _item.Title.Contains(request.Search.Value));

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.OrderBy(m => -m.Popularity).Skip(request.Start).Take(request.Length).Include(m => m.Class).Include(m => m.Language).ToList();

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return (JsonResult<IDataTablesResponse>)new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet).Data;
        }
    }
}