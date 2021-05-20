using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductID = 1, ProductTitle = "Mango", ProductImagePath = "/images/image-1.jpg", ProductAvailability = 10, ProductDemand = 3, ProductRating = 0 },
                new Product() { ProductID = 2, ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 3, ProductTitle = "Earphones", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 4, ProductTitle = "Xbox", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 5, ProductTitle = "Laptop", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 6, ProductTitle = "Office chair", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 7, ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 },
                new Product() { ProductID = 8, ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 3 }
                );
        }
    }
}
