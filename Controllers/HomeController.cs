using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using ApniShop.Models;
using ApniShop.Repositories;
using ApniShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApniShopUser> userManager;
        private readonly ApniShopContext context;

        public HomeController(IWebHostEnvironment hostingEnvironment,
                                UserManager<ApniShopUser> userManager,
                                ApniShopContext context)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> indexProductViewModels = new List<ProductViewModel>();
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
                    indexProductViewModels.Add(new ProductViewModel
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
                    indexProductViewModels.Add(new ProductViewModel
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
            IEnumerable<ProductViewModel> en = indexProductViewModels;
            return View(en);
        }

        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            var currentUserWants = await context.Wants_ProductApniShopUser
                .Where(x => x.ApniShopUser == currentUser)
                .Select(x => x.Product)
                .ToListAsync();
            List<ProductViewModel> listData = new List<ProductViewModel>();
            foreach (var product in currentUserWants)
            {
                listData.Add(new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductTitle = product.ProductTitle,
                    ProductImagePath = product.ProductImagePath,
                    ProductAvailability = product.ProductAvailability,
                    ProductDemand = product.ProductDemand,
                    ProductRating = product.ProductRating,
                    Wanted = true
                });
            }
            return View(listData);
        }

        [Authorize]
        public async Task<IActionResult> Inventory()
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            var currentUserWants = await context.Wants_ProductApniShopUser
                .Where(x => x.ApniShopUser == currentUser)
                .Select(x => x.Product)
                .ToListAsync();
            List<ProductViewModel> listData = new List<ProductViewModel>();
            foreach (var product in currentUserWants)
            {
                listData.Add(new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductTitle = product.ProductTitle,
                    ProductImagePath = product.ProductImagePath,
                    ProductAvailability = product.ProductAvailability,
                    ProductDemand = product.ProductDemand,
                    ProductRating = product.ProductRating,
                    Wanted = true
                });
            }
            return View(listData);
        }

        [Authorize]
        public async Task<IActionResult> Want(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ApniShopUser currentUser = await userManager.GetUserAsync(User);
                var currentUserWants = await context.Wants_ProductApniShopUser
                    .Where(x => x.ApniShopUser == currentUser)
                    .Select(x => x.Product)
                    .ToListAsync();
                var product = context.Products
                    .Where(x => x.ProductID == id)
                    .FirstOrDefault();
                var wantStatus = currentUserWants.Contains(product);
                if (wantStatus == true)
                {
                    context.Wants_ProductApniShopUser
                        .Remove(new Wants_ProductApniShopUser
                        {
                            ApniShopUser = currentUser,
                            Product = product
                        });
                    product.ProductDemand--;
                    context.SaveChanges();
                }
                else
                {
                    context.Wants_ProductApniShopUser
                        .Add(new Wants_ProductApniShopUser
                        {
                            ProductID = product.ProductID,
                            Product = product,
                            ApniShopUserID = currentUser.Id,
                            ApniShopUser = currentUser
                        });
                    product.ProductDemand++;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (model.ProductImage != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        await model.ProductImage.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }
                    var newProd = new Product
                    {
                        ProductTitle = model.ProductTitle,
                        ProductImagePath = uniqueFileName,
                        ProductAvailability = model.ProductAvailability,
                        ProductDemand = 0,
                        ProductRating = 0,
                        //ProductSeller = await userManager.GetUserAsync(User)
                    };
                    await context.Products.AddAsync(newProd);
                    await context.SaveChangesAsync();
                    //productRepository.Create(newProd);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
