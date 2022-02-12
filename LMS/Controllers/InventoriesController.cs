using AutoMapper;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;
using Microsoft.Ajax.Utilities;

namespace Vidly.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly LibraryManagmentContext _context;
        public InventoriesController()
        {
            _context = new LibraryManagmentContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            return View("Form");
        }
        public ActionResult Edit(int id)
        {
            var inventoryInDb = _context.Inventories.FirstOrDefault(m => m.Id == id);
            if (inventoryInDb != null)
            {
                return View("Form", inventoryInDb);
            }
            return Content("error, inventory not found");
        }
        public ActionResult Save(Inventory inventory)
        {
            if(inventory.Id != 0)
            {
                var inventoryInDb = _context.Inventories.FirstOrDefault(m => m.Id == inventory.Id);
                if(inventoryInDb != null)
                {
                    Mapper.Map(inventory, inventoryInDb);
                    if(_context.SaveChanges() > 0)
                    {
                        return RedirectToAction("Details", new { id = inventoryInDb.Id});
                    }
                    return Content("error, failed to save inventory");
                }
                return Content("error, inventory not found");
            }
            else
            {
                if(_context.Inventories.FirstOrDefault(m => m.Shelf == inventory.Shelf && m.Row == inventory.Row && m.Column == inventory.Column) != null)
                {
                    return Content("Inventory, already exists");
                }
                _context.Inventories.Add(inventory);
                if (_context.SaveChanges() > 0)
                {
                    return RedirectToAction("Details", new { id = inventory.Id });
                }
                return Content("error, failed to add inventory");
            }
        }

        public ActionResult Details(int id)
        {
            var inventory = _context.Inventories.Include(m => m.BookCopys).FirstOrDefault(m => m.Id == id);
            if (inventory.Id != 0)
            {
                return View(inventory);
            }
            return Content("error, inventory not found");
        }


        [HttpPost]
        public ActionResult Table(IDataTablesRequest request)
        {
            var columns = request.Columns as List<IColumn>;
            var data = _context.Inventories;
            var filteredData = data.Include(m => m.BookCopys);

            var column = columns.Find(m => m.Name == "Column");
            var row = columns.Find(m => m.Name == "Row");
            var shelf = columns.Find(m => m.Name == "Shelf");
            var copies = columns.Find(m => m.Name == "Copies");
            var count = _context.Inventories.Count();
            if (!String.IsNullOrWhiteSpace(request.Search.Value))
            {
                filteredData = filteredData.Where(m => (m.Shelf + "/" + m.Column + "/" + m.Row).StartsWith(request.Search.Value));
                count = filteredData.Count();
            }

            if (column.Sort != null)
            {
                if (column.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Column).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Column).Skip(request.Start);
            }
            else if (row.Sort != null)
            {
                if (row.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Row).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Row).Skip(request.Start);
            }
            else if (shelf.Sort != null)
            {
                if (shelf.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.Shelf).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.Shelf).Skip(request.Start);
            }
            else if (copies.Sort != null)
            {
                if (copies.Sort.Direction == SortDirection.Ascending)
                    filteredData = filteredData.OrderBy(m => m.BookCopys.Count).Skip(request.Start);
                else
                    filteredData = filteredData.OrderByDescending(m => m.BookCopys.Count).Skip(request.Start);
            }
            if (request.Length > 0)
            {
                filteredData = filteredData.Take(request.Length);
            }
            var filteredList = filteredData.AsEnumerable();
            filteredList.ForEach(m =>
            {
                m.BookCopys.ForEach(e =>
                {
                    e.Book = null;
                    e.Inventory = null;
                    e.Customer = null;
                });
            });
            var response = DataTablesResponse.Create(request, filteredList.Count(), count, filteredList);
            return new DataTablesJsonResult(response);
        }
    }
}