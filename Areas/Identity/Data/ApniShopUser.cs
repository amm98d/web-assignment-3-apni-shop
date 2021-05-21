using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApniShop.Models;
using Microsoft.AspNetCore.Identity;

namespace ApniShop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApniShopUser class
    public class ApniShopUser : IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<Product> Inventory { get; set; }

        public ICollection<Wants_ProductApniShopUser> Wants { get; set; }
    }
}
