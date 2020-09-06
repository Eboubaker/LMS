using AutoMapper;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class BookCopiesController : Controller
    {
        ApplicationDbContext _context;
        public BookCopiesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: BookCopies/forbook/id
        public ActionResult ForBook(int id)
        {
            var book = _context.Books.FirstOrDefault(m => m.Id == id);
            if(book == null)
            {
                return HttpNotFound("Book not found");
            }
            return View(book);
        }
        // GET: BookCopies/new/id
        public ActionResult New(int id)
        {
            if (_context.Books.FirstOrDefault(m => m.Id == id) == null)
            {
                return HttpNotFound("Book not found");
            }
            return View(new BookCopy { BookId = id});
        }
        // GET: BookCopies/edit/id
        public ActionResult Edit(int id)
        {
            var copy = _context.BookCopies.Include(m => m.Inventory).FirstOrDefault(m => m.Id == id);
            if (copy == null)
            {
                return HttpNotFound("Book not found");
            }
            return View(copy);
        }
        // POST: BookCopies/update/id
        [HttpPost]
        public ActionResult Update(BookCopy copy)
        {
            var copyInDb = _context.BookCopies.Include(m => m.Inventory).FirstOrDefault(m => m.Id == copy.Id);
            if (copyInDb == null)
            {
                return HttpNotFound("Book not found");
            }
            if(_context.Inventories.FirstOrDefault(m => m.Column == copy.Inventory.Column 
                    && m.Row == copy.Inventory.Row 
                    && m.Shelf == copy.Inventory.Shelf) == null)
            {
                return HttpNotFound("Inventory not found, Create new one");
            }
            Mapper.Map(copy, copyInDb);
            if (_context.SaveChanges() > 0)
            {

                return Json(new { success = true });
            }
            return HttpNotFound("Cant save copy");
        }
        // POST: BookCopies/add/id
        [HttpPost]
        public ActionResult Add(BookCopy copy)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound("Invalid copy state");
            }
            _context.BookCopies.Add(copy);
            if (_context.SaveChanges() > 0)
            {

                return Json(new { success = true });
            }
            return HttpNotFound("Cant add copy");
        }
        // POST: BookCopies/delete/id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var copyInDb = _context.BookCopies.FirstOrDefault(m => m.Id == id);
            if (copyInDb == null)
            {
                return HttpNotFound("book copy not found");
            }
            _context.BookCopies.Remove(copyInDb);
            if (_context.SaveChanges() > 0)
            {

                return Json(new { success = true });
            }
            return HttpNotFound("Cant remove copy");
        }
        // POST /bookcopies/table/{request}
        [HttpPost]
        public ActionResult Table(IDataTablesRequest request, int bookId)
        {
            var filteredData = Filter(request, bookId);// Process Sorting, Searching & Paging
            var response = DataTablesResponse.Create(request, filteredData.Count(), _context.BookCopies.Where(m => m.BookId == bookId).Count(), filteredData);
            return new DataTablesJsonResult(response);
        }
        private List<BookCopy> Filter(IDataTablesRequest request, int bookId)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.BookCopies;
            var filteredData = data.Include(m => m.Inventory).Where(m => m.BookId == bookId);

            var column = columns.Find(m => m.Name == "Column");
            var row = columns.Find(m => m.Name == "Row");
            var shelf = columns.Find(m => m.Name == "Shelf");
            var rented = columns.Find(m => m.Name == "Rented");

            if (column.Sort != null)
            {
                if (column.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Column);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Column);
            }
            else if (row.Sort != null)
            {
                if (row.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Row);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Row);
            }
            else if (shelf.Sort != null)
            {
                if (shelf.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Shelf);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Shelf);
            }
            else if (rented.Sort != null)
            {
                if (rented.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Rented);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Rented);
            }
            filteredData = filteredData.Skip(request.Start);
            if (request.Length > 0)
            {
                filteredData = filteredData.Take(request.Length);
            }
            return filteredData.ToList();
        }
    }
}