using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using ApniShop.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly IServiceScopeFactory scopeFactory;

        public SQLProductRepository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApniShopContext>();
                context.Products.Add(new Product() { ProductTitle = "Mango", ProductImagePath = "/images/image-1.jpg", ProductAvailability = 10, ProductDemand = 3, ProductRating = 0 });
                context.Products.Add(new Product() { ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Earphones", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Xbox", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Laptop", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Office chair", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 1 });
                context.Products.Add(new Product() { ProductTitle = "Sofa", ProductImagePath = "/images/image-2.jpg", ProductAvailability = 3, ProductDemand = 1, ProductRating = 3 });
                context.SaveChanges();
            }
        }

        public void Create(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApniShopContext>();
                return context.Products.ToList();
            }
        }
    }
}
