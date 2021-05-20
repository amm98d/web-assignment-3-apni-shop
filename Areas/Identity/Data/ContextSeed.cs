using ApniShop.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Areas.Identity.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApniShopUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed roles
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Customer"));
        }

        public static async Task SeedAdminAsync(UserManager<ApniShopUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed the default admin user
            var defaultUser = new ApniShopUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FullName = "Admin",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "admin");
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }
            }
        }
    }
}
