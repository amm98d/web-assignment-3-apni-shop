﻿using ApniShop.Data;
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
                    ProductImagePath = "image-1.jpg",
                    ProductAvailability = 10,
                    ProductDemand = 3,
                    ProductRating = 0,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 2,
                    ProductTitle = "Sofa",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTitle = "Earphones",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTitle = "Xbox",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTitle = "Laptop",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTitle = "Office chair",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 7,
                    ProductTitle = "Sofa",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 1,
                    Approved = true,
                },
                new Product()
                {
                    ProductId = 8,
                    ProductTitle = "Sofa",
                    ProductImagePath = "image-2.jpg",
                    ProductAvailability = 3,
                    ProductDemand = 1,
                    ProductRating = 3,
                    Approved = true,
                }
                );
        }
    }
}
