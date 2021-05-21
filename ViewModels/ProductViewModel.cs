using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Display(Name = "Title")]
        public string ProductTitle { get; set; }

        [Display(Name = "Image")]
        public string ProductImagePath { get; set; }

        [Display(Name = "Availability")]
        public int ProductAvailability { get; set; }

        [Display(Name = "Demand")]
        public int ProductDemand { get; set; }

        [Display(Name = "Rating")]
        public int ProductRating { get; set; }

        public bool Wanted { get; set; }

        [Display(Name = "Status")]
        public bool ApprovalStatus { get; set; }
    }
}
