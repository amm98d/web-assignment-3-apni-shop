using ApniShop.Areas.Identity.Data;
using ApniShop.Data;
using ApniShop.Models;
using ApniShop.Repositories;
using ApniShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApniShopUser> userManager;
        private readonly ApniShopContext context;

        public ProductsController(IWebHostEnvironment hostingEnvironment,
                                UserManager<ApniShopUser> userManager,
                                ApniShopContext context)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.context = context;
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> Want(int id)
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
                        .Remove(new Wants_ProductApniShopUser {
                            ApniShopUser = currentUser, Product = product 
                        });
                    currentUserWants.Remove(product);
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
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel model)
        //public async Task<ActionResult> Create(CreateProductViewModel model)
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
                        model.ProductImage.CopyTo(new FileStream(filePath, FileMode.Create));
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
                    context.Products.Add(newProd);
                    context.SaveChanges();
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
