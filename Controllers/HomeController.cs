using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using ApniShop.Models;
using ApniShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Product> allApprovedProducts = await context.Products
                .Where(x => x.Approved == true)
                .ToListAsync();
            if (User.Identity.IsAuthenticated)
            {
                ApniShopUser currentUser = await userManager.GetUserAsync(User);
                foreach (var product in allApprovedProducts)
                {
                    var currentUserWants = await context.Wants_ProductApniShopUser
                        .Where(x => x.ApniShopUser == currentUser)
                        .Select(x => x.Product)
                        .ToListAsync();
                    indexProductViewModels.Add(new ProductViewModel
                    {
                        ProductID = product.ProductId,
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
                foreach (var product in allApprovedProducts)
                {
                    indexProductViewModels.Add(new ProductViewModel
                    {
                        ProductID = product.ProductId,
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
                    ProductTitle = product.ProductTitle,
                    ProductImagePath = product.ProductImagePath,
                    ProductAvailability = product.ProductAvailability,
                    ProductDemand = product.ProductDemand,
                    ProductRating = product.ProductRating,
                });
            }
            return View(listData);
        }

        [Authorize]
        public async Task<IActionResult> Inventory()
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            List<Product> inventory = new List<Product>();
            if (currentUser.Email == "admin@admin.com")
            {
                inventory = await context.Products
                    .Where(x => x.ProductSeller == null || x.ProductSeller == currentUser)
                    .ToListAsync();
            }
            else
            {
                inventory = await context.Products
                    .Where(x => x.ProductSeller == currentUser)
                    .ToListAsync();
            }
            List<ProductViewModel> listData = new List<ProductViewModel>();
            foreach (var product in inventory)
            {
                listData.Add(new ProductViewModel
                {
                    ProductID = product.ProductId,
                    ProductTitle = product.ProductTitle,
                    ProductImagePath = product.ProductImagePath,
                    ProductAvailability = product.ProductAvailability,
                    ProductDemand = product.ProductDemand,
                    ProductRating = product.ProductRating,
                    ApprovalStatus = product.Approved,
                });
            }
            return View(listData);
        }

        [Authorize]
        public async Task<IActionResult> Want(int id)
        {
            var product = context.Products
                .Where(x => x.ProductId == id)
                .FirstOrDefault();
            if (product.Approved == false)
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            var currentUserWants = await context.Wants_ProductApniShopUser
                .Where(x => x.ApniShopUser == currentUser)
                .Select(x => x.Product)
                .ToListAsync();
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
                        ProductID = product.ProductId,
                        Product = product,
                        ApniShopUserID = currentUser.Id,
                        ApniShopUser = currentUser
                    });
                product.ProductDemand++;
                context.SaveChanges();
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
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProductImage.CopyToAsync(fileStream);
                        }
                    }
                    var currentUser = await userManager.GetUserAsync(User);
                    var newProd = new Product
                    {
                        ProductTitle = model.ProductTitle,
                        ProductImagePath = uniqueFileName,
                        ProductAvailability = model.ProductAvailability,
                        ProductDemand = 0,
                        ProductRating = 0,
                        ProductSellerId = currentUser.Id,
                        ProductSeller = currentUser,
                        Approved = currentUser.Email == "admin@admin.com",
                    };
                    await context.Products.AddAsync(newProd);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            var currentUserInventory = await context.Products
                .Where(x => x.ProductSeller == currentUser)
                .ToListAsync();
            var product = await context.Products
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();
            if (!currentUserInventory.Contains(product) && currentUser.Email != "admin@admin.com")
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
            else
            {
                EditProductViewModel viewModel = new EditProductViewModel
                {
                    ProductID = id,
                    ProductTitle = product.ProductTitle,
                    ProductAvailability = product.ProductAvailability,
                    ProductImagePath = product.ProductImagePath
                };
                return View(viewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            try
            {
                ApniShopUser currentUser = await userManager.GetUserAsync(User);
                var currentUserInventory = await context.Products
                    .Where(x => x.ProductSeller == currentUser)
                    .ToListAsync();
                var product = await context.Products
                    .Where(x => x.ProductId == model.ProductID)
                    .FirstOrDefaultAsync();
                if (!currentUserInventory.Contains(product) && currentUser.Email != "admin@admin.com")
                {
                    return LocalRedirect("/Identity/Account/AccessDenied");
                }
                else
                {
                    if (model.ProductImage != null && product.ProductImagePath != model.ProductImage.FileName)
                    {
                        string newUniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", newUniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProductImage.CopyToAsync(fileStream);
                        }
                        product.ProductImagePath = newUniqueFileName;
                    }
                    product.ProductTitle = model.ProductTitle;
                    product.ProductAvailability = model.ProductAvailability;
                    product.Approved = false;
                    await context.SaveChangesAsync();
                    return RedirectToAction("Inventory");
                }
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnApproved()
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            List<Product> unapproved = await context.Products
                .Where(x => x.Approved == false)
                .ToListAsync();
            List<ProductViewModel> listData = new List<ProductViewModel>();
            foreach (var product in unapproved)
            {
                listData.Add(new ProductViewModel
                {
                    ProductID = product.ProductId,
                    ProductTitle = product.ProductTitle,
                    ProductImagePath = product.ProductImagePath,
                    ProductAvailability = product.ProductAvailability,
                });
            }
            return View(listData);
        }

        [Authorize]
        public async Task<IActionResult> Approve(int id)
        {
            var product = await context.Products
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();
            product.Approved = true;
            context.SaveChanges();
            return RedirectToAction("UnApproved");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            ApniShopUser currentUser = await userManager.GetUserAsync(User);
            var currentUserInventory = await context.Products
                .Where(x => x.ProductSeller == currentUser)
                .ToListAsync();
            var product = await context.Products
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();
            if (!currentUserInventory.Contains(product) && currentUser.Email != "admin@admin.com")
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Inventory");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
