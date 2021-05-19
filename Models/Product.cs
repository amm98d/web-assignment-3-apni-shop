using ApniShop.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImagePath { get; set; }
        public int ProductAvailability { get; set; }
        public int ProductDemand { get; set; }
        public int ProductRating { get; set; }
        public ApniShopUser ProductSeller { get; set; }
    }
}
