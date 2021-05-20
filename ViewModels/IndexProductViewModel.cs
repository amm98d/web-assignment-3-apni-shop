using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.ViewModels
{
    public class IndexProductViewModel
    {
        public int ProductID { get; set; }

        public string ProductTitle { get; set; }

        public string ProductImagePath { get; set; }

        public int ProductAvailability { get; set; }

        public int ProductDemand { get; set; }

        public int ProductRating { get; set; }

        public bool Wanted { get; set; }
    }
}
