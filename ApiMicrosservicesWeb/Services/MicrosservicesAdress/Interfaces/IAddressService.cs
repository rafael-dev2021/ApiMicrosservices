using ApiMicrosservicesWeb.Models.MicrosservicesAdress;

namespace ApiMicrosservicesWeb.Services.MicrosservicesAdress.Interfaces
{
    public interface IAddressService
    {
        Task<AddressDtoViewModel> GetCepAsync(string cep);
        Task<AddressDtoViewModel> GetAsync(string cep);
        Task<AddressDtoViewModel> CreateAddressAsync(AddressDtoViewModel newAddress);
        Task<List<AddressDtoViewModel>> GetAllAddressAsync();
    }
}
