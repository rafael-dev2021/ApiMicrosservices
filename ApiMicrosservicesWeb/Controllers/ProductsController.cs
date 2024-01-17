using ApiMicrosservicesWeb.Models.MicrosserviceProduct;
using ApiMicrosservicesWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiMicrosservicesWeb.Controllers;

public class ProductsController(IProductViewModelService productViewModelService) : Controller
{
    private readonly IProductViewModelService _productViewModelService = productViewModelService;

    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var produts = await _productViewModelService.GetAllProducts();
        return View(produts);
    }
}
