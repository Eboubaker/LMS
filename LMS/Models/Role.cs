using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
namespace LMS.Models
{
    public static class Role
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
        public static async Task<bool> AddSuperAdminRoles(RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            // Add all priviliges
            foreach (var prob in typeof(Role).GetProperties())
            {
                if (prob.Name.StartsWith("Can"))
                {
                    await AddToRole(prob.GetValue(null, null).ToString(), manager, userManager, user);
                }
            }
            //await AddToRole(CanEditBooks, manager, userManager, user);
            //await AddToRole(CanAddBooks, manager, userManager, user);
            //await AddToRole(CanDeleteBooks, manager, userManager, user);
            //await AddToRole(CanViewBooks, manager, userManager, user);

            //await AddToRole(CanEditCustomers, manager, userManager, user);
            //await AddToRole(CanAddCustomers, manager, userManager, user);
            //await AddToRole(CanDeleteCustomers, manager, userManager, user);
            //await AddToRole(CanViewCustomers, manager, userManager, user);

            return true;
        }
        public static async Task<bool> AddStandardUserRoles(RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            await AddToRole(CanViewBooks, manager, userManager, user);
            return true;
        }
        private static async Task<bool> AddToRole(string role, RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
            if (await manager.FindByNameAsync(role) == null)
            {
                await manager.CreateAsync(new IdentityRole(role));
            }
            if (!userManager.IsInRole(user.Id, role))
            {
                await userManager.AddToRoleAsync(user.Id, role);
            }
            return true;
        }
    }
}