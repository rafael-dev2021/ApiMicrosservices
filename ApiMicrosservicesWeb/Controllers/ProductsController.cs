using ApiMicrosservicesWeb.Models.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApiMicrosservicesWeb.Controllers
{
    public class ProductsController(IProductService productService,
                            ICategoryService categoryService,
                            IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {

            var result = await _productService.GetAllProducts(await GetAccessToken());

            if (result is null)
                return View("Index");

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductAsync(productVM, await GetAccessToken());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                                     _categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await
                               _categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");

            var result = await _productService.GetByProductIdAsync(id, await GetAccessToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(productVM, await GetAccessToken());

                return RedirectToAction("Index");
            }
            return View(productVM);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            var result = await _productService.GetByProductIdAsync(id, await GetAccessToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var result = await _productService.DeleteProductAsync(id, await GetAccessToken());

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }
    }
}
