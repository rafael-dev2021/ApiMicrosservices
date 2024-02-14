using ApiMicrosservicesWeb.Models;
using ApiMicrosservicesWeb.Models.MicrosservicesProduct;
using ApiMicrosservicesWeb.Models.MicrosservicesShoppingCart;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
using ApiMicrosservicesWeb.Services.MicrosservicesShoppingCart.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiMicrosservicesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger,
            IProductService productService,
            ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts(string.Empty);

            if (products is null)
            {
                return View("Error");
            }

            return View(products);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.GetByProductIdAsync(id, token);

            if (product is null)
                return View("Error");

            return View(product);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetailsPost
            (ProductViewModel productVM)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new()
            {
                CartHeader = new CartHeaderViewModel
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartItemViewModel cartItem = new()
            {
                Quantity = productVM.Quantity,
                ProductId = productVM.Id,
                Product = await _productService.GetByProductIdAsync(productVM.Id, token)
            };

            List<CartItemViewModel> cartItemsVM = new List<CartItemViewModel>();
            cartItemsVM.Add(cartItem);
            cart.CartItems = cartItemsVM;

            var result = await _cartService.AddItemToCartAsync(cart, token);

            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productVM);
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
