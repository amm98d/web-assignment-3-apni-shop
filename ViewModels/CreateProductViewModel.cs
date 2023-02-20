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
        [Required(ErrorMessage = "The Product Title is required.")]
        [Display(Name ="Product Title")]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "The Product Image is required.")]
        [Display(Name = "Upload Product Image")]
        public IFormFile ProductImage { get; set; }

        [Display(Name = "Product Availability (Quantity)")]
        public int ProductAvailability { get; set; }
    }
}
