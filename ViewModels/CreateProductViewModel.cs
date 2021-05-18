using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name ="Product Title")]
        public string ProductTitle { get; set; }

        [Required]
        [Display(Name = "Upload Product Image")]
        public IFormFile ProductImage { get; set; }

        [Display(Name = "Product Availability (Quantity)")]
        public int ProductAvailability { get; set; }
    }
}
