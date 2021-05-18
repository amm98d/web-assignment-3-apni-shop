using ApniShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(int Id);

        List<Product> GetProducts();
    }
}
