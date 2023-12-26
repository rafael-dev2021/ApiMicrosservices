using ApiMicrosservicesWeb.Models.MicrosservicesAdress;
using ApiMicrosservicesWeb.Services.MicrosservicesAdress.Interfaces;
using System.Text;
using System.Text.Json;

namespace ApiMicrosservicesWeb.Services.MicrosservicesAdress
{
    public class AddressDtoService : IAddressService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private const string apiEndpoint = "/api/address/";
        private const string apiEndpointCep = "/api/address/{cep}";
        private AddressDtoViewModel adressDtoView;

        public AddressDtoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<AddressDtoViewModel>> GetAllAddressAsync()
        {
            var client = _clientFactory.CreateClient("AddressApi");

            using var response = await client.GetAsync(apiEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var addressDtoViewModels = await JsonSerializer.DeserializeAsync<List<AddressDtoViewModel>>(apiResponse, _options);
                return addressDtoViewModels ?? [];
            }
            else
            {
                return [];
            }
        }

        public async Task<AddressDtoViewModel> CreateAddressAsync(AddressDtoViewModel newAddress)
        {
            var client = _clientFactory.CreateClient("AddressApi");

            StringContent content = new(JsonSerializer.Serialize(newAddress), Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(apiEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                newAddress = await JsonSerializer.DeserializeAsync<AddressDtoViewModel>(apiResponse, _options);
                return newAddress;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AddressDtoViewModel> GetAsync(string cep)
        {
            var client = _clientFactory.CreateClient("AddressApi");

            using var response = await client.GetAsync(apiEndpointCep.Replace("{id}", cep.ToString()));
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                adressDtoView = await JsonSerializer.DeserializeAsync<AddressDtoViewModel>(apiResponse, _options);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return adressDtoView;
        }

        public async Task<AddressDtoViewModel> GetCepAsync(string cep)
        {
            var client = _clientFactory.CreateClient("AddressApi");

            using var response = await client.GetAsync(apiEndpointCep.Replace("{cep}", cep));
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var address = await JsonSerializer.DeserializeAsync<AddressDtoViewModel>(apiResponse, _options);
                return address;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

    }
}
