using ApniShop.Areas.Identity.Data;
using ApniShop.Models;
using ApniShop.Repositories;
using ApniShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApniShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApniShopUser> userManager;

        public ProductsController(IProductRepository productRepository, 
                                IWebHostEnvironment hostingEnvironment,
                                UserManager<ApniShopUser> userManager)
        {
            this.productRepository = productRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel model)
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
                        ProductSeller = await userManager.GetUserAsync(User)
                };
                    productRepository.Create(newProd);
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
