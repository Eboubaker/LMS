using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Collections;

namespace LMS.Models
{
    public class Role
    {
        public const string CanModifyRoles = "CanModifyRoles";

        public const string CanEditBooks = "CanEditBooks";
        public const string CanAddBooks = "CanAddBooks";
        public const string CanDeleteBooks = "CanDeleteBooks";
        public const string CanViewBooks = "CanViewBooks";

        public const string CanEditCustomers = "CanEditCustomers";
        public const string CanAddCustomers = "CanAddCustomers";
        public const string CanDeleteCustomers = "CanDeleteCustomers";
        public const string CanViewCustomers = "CanViewCustomers";

        public const string CanViewRentals = "CanViewRentals";
        public const string CanAddRentals = "CanAddRentals";
        public const string CanEditRentals = "CanEditRentals";
        public const string CanDeleteRentals = "CanDeleteRentals";

        public const string CanViewBookCopies = "CanViewBookCopies";
        public const string CanAddBookCopies = "CanAddBookCopies";
        public const string CanEditBookCopies = "CanEditBookCopies";
        public const string CanDeleteBookCopies = "CanDeleteBookCopies";

        public const string CanViewInventories = "CanViewInventories";
        public const string CanAddInventories = "CanAddInventories";
        public const string CanEditInventories = "CanEditInventories";
        public const string CanDeleteInventories = "CanDeleteInventories";
        public static async Task AddSuperAdminRoles(RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            await AddToRole(CanModifyRoles, manager, userManager, user);

            await AddToRole(CanEditBooks, manager, userManager, user);
            await AddToRole(CanAddBooks, manager, userManager, user);
            await AddToRole(CanDeleteBooks, manager, userManager, user);
            await AddToRole(CanViewBooks, manager, userManager, user);

            await AddToRole(CanEditCustomers, manager, userManager, user);
            await AddToRole(CanAddCustomers, manager, userManager, user);
            await AddToRole(CanDeleteCustomers, manager, userManager, user);
            await AddToRole(CanViewCustomers, manager, userManager, user);

            await AddToRole(CanViewRentals, manager, userManager, user);
            await AddToRole(CanAddRentals, manager, userManager, user);
            await AddToRole(CanDeleteRentals, manager, userManager, user);
            await AddToRole(CanEditRentals, manager, userManager, user);

            await AddToRole(CanEditBookCopies, manager, userManager, user);
            await AddToRole(CanAddBookCopies, manager, userManager, user);
            await AddToRole(CanDeleteBookCopies, manager, userManager, user);
            await AddToRole(CanViewBookCopies, manager, userManager, user);

            await AddToRole(CanViewInventories, manager, userManager, user);
            await AddToRole(CanAddInventories, manager, userManager, user);
            await AddToRole(CanDeleteInventories, manager, userManager, user);
            await AddToRole(CanEditInventories, manager, userManager, user);
        }
        public static async Task AddStandardUserRoles(RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            await AddToRole(CanViewBooks, manager, userManager, user);
        }
        private static async Task AddToRole(string role, RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            if (await manager.FindByNameAsync(role) == null)
            {
                await manager.CreateAsync(new IdentityRole(role));
            }
            if (!userManager.IsInRole(user.Id, role))
            {
                await userManager.AddToRoleAsync(user.Id, role);
            }
        }
    }
}