using ApiMicrosservicesAddress.DTOs;

namespace ApiMicrosservicesAddress.Services.Interfaces;

public interface IAddressService
{
    Task<AddressDto> GetCepAsync(string cep);
    Task<AddressDto> GetAsync(string cep);
    Task CreateAddressAsync(AddressDto newAddress);
    Task<List<AddressDto>> GetAllAddressAsync();
}
