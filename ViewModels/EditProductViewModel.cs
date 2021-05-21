using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.ViewModels
{
    public class EditProductViewModel : CreateProductViewModel
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Upload Product Image")]
        public string ProductImagePath { get; set; }
    }
}
