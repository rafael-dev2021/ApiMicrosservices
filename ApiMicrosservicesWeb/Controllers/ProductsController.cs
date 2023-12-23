using ApiMicrosservicesWeb.Models.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {

            var result = await _productService.GetAllProducts();

            if (result is null)
                return View("Index");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductAsync(productVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                                     _categoryService.GetAllCategories(), "Id", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int? id)
        {
            ViewBag.CategoryId = new SelectList(await
                               _categoryService.GetAllCategories(), "Id", "Name");

            var result = await _productService.GetByProductIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(productVM);

                return RedirectToAction("Index");
            }
            return View(productVM);
        }


        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int? id)
        {
            var result = await _productService.GetByProductIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }
    }
}
