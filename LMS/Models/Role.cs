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
        private static FieldInfo[] GetConstants(System.Type type)
        {
            ArrayList constants = new ArrayList();

            FieldInfo[] fieldInfos = type.GetFields(
                // Gets all public and static fields

                BindingFlags.Public | BindingFlags.Static |
                // This tells it to get the fields from all base types as well

                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach (FieldInfo fi in fieldInfos)
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determines if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if (fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi);

            // Return an array of FieldInfos
            return (FieldInfo[])constants.ToArray(typeof(FieldInfo));
        }
        public static async Task<bool> AddSuperAdminRoles(RoleManager<IdentityRole> manager, ApplicationUserManager userManager, ApplicationUser user)
        {
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