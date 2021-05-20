using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using ApniShop.Models;
using ApniShop.Repositories;
using ApniShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApniShopUser> userManager;
        private readonly ApniShopContext context;

        public HomeController(UserManager<ApniShopUser> userManager,
            ApniShopContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<IndexProductViewModel> indexProductViewModels = new List<IndexProductViewModel>();
            List<Product> allProducts = await context.Products.ToListAsync();
            if (User.Identity.IsAuthenticated)
            {
                ApniShopUser currentUser = await userManager.GetUserAsync(User);
                foreach (var product in allProducts)
                {
                    var currentUserWants = await context.Wants_ProductApniShopUser
                        .Where(x => x.ApniShopUser == currentUser)
                        .Select(x => x.Product)
                        .ToListAsync();
                    indexProductViewModels.Add(new IndexProductViewModel
                    {
                        ProductID = product.ProductID,
                        ProductTitle = product.ProductTitle,
                        ProductImagePath = product.ProductImagePath,
                        ProductAvailability = product.ProductAvailability,
                        ProductDemand = product.ProductDemand,
                        ProductRating = product.ProductRating,
                        Wanted = currentUserWants.Contains(product)
                    });
                }
            }
            else
            {
                foreach (var product in allProducts)
                {
                    indexProductViewModels.Add(new IndexProductViewModel
                    {
                        ProductID = product.ProductID,
                        ProductTitle = product.ProductTitle,
                        ProductImagePath = product.ProductImagePath,
                        ProductAvailability = product.ProductAvailability,
                        ProductDemand = product.ProductDemand,
                        ProductRating = product.ProductRating,
                        Wanted = false
                    });
                }
            }
            IEnumerable<IndexProductViewModel> en = indexProductViewModels;
            return View(en);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
