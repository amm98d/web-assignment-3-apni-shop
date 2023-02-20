using ApniShop.Data;
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
                new Product()
                {
                    ProductId = 1,
                    ProductTitle = "Mango",
                    ProductImagePath = "mango.jpg",
                    ProductAvailability = 10,
                    ProductDemand = 3,
                    ProductRating = 5,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 2,
                    ProductTitle = "Perfume",
                    ProductImagePath = "perfume.jpg",
                    ProductAvailability = 1,
                    ProductDemand = 0,
                    ProductRating = 4,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTitle = "Airpods",
                    ProductImagePath = "airpods.jpg",
                    ProductAvailability = 5,
                    ProductDemand = 10,
                    ProductRating = 4,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTitle = "Controller",
                    ProductImagePath = "controller.jpg",
                    ProductAvailability = 2,
                    ProductDemand = 1,
                    ProductRating = 3,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTitle = "Burger",
                    ProductImagePath = "burger.jpg",
                    ProductAvailability = 25,
                    ProductDemand = 50,
                    ProductRating = 5,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTitle = "Shirt",
                    ProductImagePath = "shirt.jpg",
                    ProductAvailability = 15,
                    ProductDemand = 8,
                    ProductRating = 4,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 7,
                    ProductTitle = "Shoes",
                    ProductImagePath = "shoes.jpg",
                    ProductAvailability = 0,
                    ProductDemand = 0,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 8,
                    ProductTitle = "Watch",
                    ProductImagePath = "watch.jpg",
                    ProductAvailability = 5,
                    ProductDemand = 1,
                    ProductRating = 2,
                    Approved = true,
                }
                );
        }
    }
}
