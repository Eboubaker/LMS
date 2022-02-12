using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS.Models;

namespace LMS.Models
{
    

    public class LibraryManagmentContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<OldBook> OldBooks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public LibraryManagmentContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static LibraryManagmentContext Create()
        {
            return new LibraryManagmentContext();
        }
    }
}