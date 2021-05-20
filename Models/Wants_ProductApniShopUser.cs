using ApniShop.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models
{
    public class Wants_ProductApniShopUser
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public string ApniShopUserID { get; set; }
        public ApniShopUser ApniShopUser { get; set; }
    }
}
