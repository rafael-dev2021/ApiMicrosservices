using ApiMicrosservicesAddress.Context;
using ApiMicrosservicesAddress.DTOs;
using ApiMicrosservicesAddress.Errors;
using ApiMicrosservicesAddress.Providers.Interfaces;
using ApiMicrosservicesAddress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ApiMicrosservicesAddress.Services;

public class AddressService(IHttpClient httpClient, AppDbContext appDbContext) : IAddressService
{
    private readonly IHttpClient _httpClient = httpClient;
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<AddressDto> GetCepAsync(string cep)
    {
        var responseDb = await GetAsync(cep).ConfigureAwait(false);
        if (responseDb != null)
            return responseDb;

        // Obtém resposta do serviço externo
        var response = await _httpClient.GetCepAsync(cep).ConfigureAwait(false);

        // Deserializa a resposta do serviço externo com o JsonSerializerOptions.PropertyNameCaseInsensitive
        var newCep = JsonSerializer.Deserialize<AddressDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        await CreateAddressAsync(newCep).ConfigureAwait(false);
        return newCep;
    }
    public async Task<List<AddressDto>> GetAllAddressAsync()
    {
        var allAddressFound = await _appDbContext.AddressDtos
            .ToListAsync()
            .ConfigureAwait(false);

        if (!allAddressFound.Any())
        {
            throw new RequestException(new RequestError
            {
                Message = "Não há endereços cadastrados!",
                Severity = "Error",
                StatusCode = System.Net.HttpStatusCode.NotFound
            });
        }
        return allAddressFound;
    }

    public async Task<AddressDto> GetAsync(string cep) =>
        await _appDbContext.AddressDtos
        .FirstOrDefaultAsync(x => x.Cep.ToLower().Contains(cep.ToLower())).ConfigureAwait(false);


    public async Task CreateAddressAsync(AddressDto newAddress)
    {
        _appDbContext.AddressDtos.Add(newAddress);
        await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
