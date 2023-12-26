using ApiMicrosservicesWeb.Models.MicrosservicesAdress;
using ApiMicrosservicesWeb.Services.MicrosservicesAdress.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiMicrosservicesWeb.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressDtoViewModel newAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAddress = await _addressService.CreateAddressAsync(newAddress);
                return RedirectToAction("Details", new { cep = createdAddress.Cep }); // Redireciona para a página de detalhes do endereço criado
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar endereço: {ex.Message}");
            }
        }

        public async Task<IActionResult> Details(string cep)
        {
            try
            {
                var address = await _addressService.GetAsync(cep); // Obtém detalhes do endereço pelo CEP
                return View(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter detalhes do endereço: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetByCep(string cep)
        {
            try
            {
                var address = await _addressService.GetCepAsync(cep);
                return View("Details", address);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "O CEP inserido é inválido. Por favor, informe o CEP sem caracteres especiais ou hífens.");
                return View("Index"); // Volta para a página Index ou a página desejada, de acordo com sua lógica
            }
        }

    }
}
