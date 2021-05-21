using ApniShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Repositories
{
    public class ArrayProductRepository : IProductRepository
    {
        private List<Product> _productList;

        public ArrayProductRepository()
        {
            _productList = new List<Product>()
            {
                new Product(){ProductTitle="Mango", ProductImagePath="/images/image-1.jpg", ProductAvailability=10, ProductDemand=3, ProductRating=0},
                new Product(){ProductTitle="Sofa", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Earphones", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Xbox", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Laptop", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Office chair", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Sofa", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=1},
                new Product(){ProductTitle="Sofa", ProductImagePath="/images/image-2.jpg", ProductAvailability=3, ProductDemand=1, ProductRating=3},
            };
        }

        public void Create(Product newProduct)
        {
            _productList.Add(newProduct);
        }

        public Product GetProduct(int Id)
        {
            return _productList.FirstOrDefault(p => p.ProductId == Id);
        }
        public List<Product> GetProducts()
        {
            return _productList.OrderByDescending(o => o.ProductRating).ToList();
        }


    }
}
