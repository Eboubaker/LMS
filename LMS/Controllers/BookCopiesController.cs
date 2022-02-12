using AutoMapper;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using LMS.Models;
using LMS.ViewModels;
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

namespace LMS.Controllers
{
    public class BookCopiesController : Controller
    {
        LibraryManagmentContext _context;
        public BookCopiesController()
        {
            _context = new LibraryManagmentContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Details(int id)
        {
            var copy = _context.BookCopies.Include(m => m.Inventory).Include(m => m.Book).FirstOrDefault(m => m.Id == id);
            if (copy == null)
            {
                return Content("Error, Book copy not found #" + id);
            }
            if (copy.Rented)
            {
                copy.Customer = _context.Rentals.Include(m => m.Customer).FirstOrDefault(m => m.BookCopyId == id).Customer;
            }
            return View(copy);
        }
        public ActionResult ForInventory(int id)
        {
            var model = _context.Inventories.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return Content("Error, Inventory not found #" + id);
            }
            return View(model);
        }

        // GET: BookCopies/forbook/id
        public ActionResult ForBook(int id)
        {
            var book = _context.Books.FirstOrDefault(m => m.Id == id);
            if(book == null)
            {
                return Content("Error, Book not found #" + id);
            }
            var model = new BookCopiesViewModel()
            {
                Book = book
            };
            return View(model);
        }
        public ActionResult Choose(int bookId, int? customerId)
        {
            var book = _context.Books.FirstOrDefault(m => m.Id == bookId);
            if (book == null)
            {
                return HttpNotFound("Book not found");
            }
            if(customerId.HasValue && null == _context.Customers.FirstOrDefault(m => m.Id == customerId))
            {
                return Content("Error, Customer not found #" + customerId.Value);
            }
            var model = new BookCopiesViewModel();
            model.Book = book;
            model.RentalForm = new NewRentalViewModel() { BookId = book.Id, CustomerId = customerId };
            return View("ForBook", model);
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
            var copy = _context.BookCopies.Include(m => m.Inventory).Include(m => m.Book).FirstOrDefault(m => m.Id == id);
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
            var inventory = _context.Inventories.Include(m => m.BookCopys).FirstOrDefault(m => m.Column == copy.Inventory.Column
                    && m.Row == copy.Inventory.Row
                    && m.Shelf == copy.Inventory.Shelf);
            if (inventory == null)
            {
                return Content("Error, Inventory not found, Create new one");
            }
            if(inventory.Id != copy.InventoryId && inventory.BookCopys.Count >= inventory.Size)
            {
                return Content("Error, Inventory #" + inventory.Id+ " is full");
            }
            Mapper.Map(copy, copyInDb);
            if (_context.SaveChanges() > 0)
            {

                return RedirectToAction("Details", new { id = copy.Id });
            }
            return HttpNotFound("Cant save copy");
        }
        // POST: BookCopies/add/id
        [HttpPost]
        public ActionResult Add(BookCopy copy)
        {
            var inventory = _context.Inventories.Include(m => m.BookCopys).FirstOrDefault(m => m.Column == copy.Inventory.Column
                    && m.Row == copy.Inventory.Row
                    && m.Shelf == copy.Inventory.Shelf);
            if (inventory == null)
            {
                return Content("Error, Inventory not found, Create new one");
            }
            if (inventory.Id != copy.InventoryId && inventory.BookCopys.Count >= inventory.Size)
            {
                return Content("Error, Inventory #" + inventory.Id + " is full");
            }
            _context.BookCopies.Add(copy);
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Details", new { id = copy.Id });
            }
            return HttpNotFound("Cant save copy");
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
        public ActionResult Table(IDataTablesRequest request, int? bookId, int? inventoryId)
        {
            
            
            int count = 0;
            var filteredlist = new List<BookCopy>();
            DataTablesResponse response;
            if (inventoryId.HasValue)
            {
                var v = _context.Inventories.Include(m => m.BookCopys).FirstOrDefault(m => m.Id == inventoryId);
                filteredlist =  v == null ? new List<BookCopy>() : v.BookCopys.OrderByDescending(m => m.Rented).ToList();
                filteredlist.ForEach(m =>
                {
                    m.Inventory.BookCopys = null;
                    m.Book = null;
                    if (m.Rented)
                    {
                        m.Customer = _context.Rentals.Include(e => e.Customer).First(e => e.BookCopyId == m.Id).Customer;
                        m.Customer.Rentals = null;
                    }
                });
                count = filteredlist.Count;
                response = DataTablesResponse.Create(request, filteredlist.Count(), count, filteredlist);
                return new DataTablesJsonResult(response);
            }
            var columns = request.Columns as List<IColumn>;
            var data = _context.BookCopies;
            
            var filteredData = data.Include(m => m.Inventory).Where(m => m.BookId == bookId);
            count = filteredData.Count();
            var column = columns.Find(m => m.Name == "Column");
            var row = columns.Find(m => m.Name == "Row");
            var shelf = columns.Find(m => m.Name == "Shelf");
            var rented = columns.Find(m => m.Name == "Rented");
            
            if (column.Sort != null)
            {
                if (column.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Column).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Column).Skip(request.Start);
            }
            else if (row.Sort != null)
            {
                if (row.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Row).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Row).Skip(request.Start);
            }
            else if (shelf.Sort != null)
            {
                if (shelf.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Inventory.Shelf).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Inventory.Shelf).Skip(request.Start);
            }
            if (request.Length > 0 && rented.Sort == null)
            {
                filteredData = filteredData.Take(request.Length);
            }
            filteredlist = filteredData.ToList();
            if (rented.Sort != null)
            {
                if (rented.Sort.Direction == SortDirection.Ascending)
                    filteredlist = filteredlist.OrderBy(m => m.Rented).Skip(request.Start).ToList();
                else
                    filteredlist = filteredlist.OrderByDescending(m => m.Rented).Skip(request.Start).ToList();
                if (request.Length > 0)
                    filteredlist = filteredlist.Take(request.Length).ToList();
            }
            filteredlist.ForEach(m => {
                m.Inventory.BookCopys = null;
                if (m.Rented)
                {
                    m.Customer = _context.Rentals.Include(e => e.Customer).FirstOrDefault(e => e.BookCopyId == m.Id).Customer;
                    m.Customer.Rentals = null;
                }
            });
            response = DataTablesResponse.Create(request, filteredlist.Count(), count, filteredlist);
            return new DataTablesJsonResult(response);
        }
    }
}