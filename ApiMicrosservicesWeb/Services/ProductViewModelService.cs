using ApiMicrosservicesWeb.Models.MicrosserviceProduct;
using ApiMicrosservicesWeb.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace ApiMicrosservicesWeb.Services;

public class ProductViewModelService : IProductViewModelService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private readonly IDistributedCache _cache;
    private const string apiEndpoint = "/api/v1/products/";
    private const string apiEndpointId = "/api/v1/products/{id}";

    public ProductViewModelService(IHttpClientFactory clientFactory, IDistributedCache cache)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _cache = cache;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using var response = await client.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var productsViewModels = await JsonSerializer
                .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);

            return productsViewModels;
        }
        else
        {
            return Enumerable.Empty<ProductViewModel>();
        }
    }

    public async Task<ProductViewModel> GetbyIdAsync(int? id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productVM;

        using var response = await client.GetAsync(apiEndpointId.Replace("{id}", id.ToString()));
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            productVM = await JsonSerializer
                .DeserializeAsync<ProductViewModel>(apiResponse, _options);
        }
        else
        {
            throw new HttpRequestException(response.ReasonPhrase);
        }
        return productVM;
    }

    public async Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        StringContent content = new(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productViewModel = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
        return productViewModel;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using var response = await client.DeleteAsync($"{apiEndpoint}{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<ProductViewModel> UpdateAsync(ProductViewModel productViewModel)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productVm = null;

        using var response = await client.PutAsJsonAsync(apiEndpointId.Replace("{id}", productViewModel.Id.ToString()), productViewModel);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(apiResponse))
            {
                productVm = JsonSerializer.Deserialize<ProductViewModel>(apiResponse, _options);
            }
        }
        else
        {
            throw new HttpRequestException(response.ReasonPhrase);
        }
        return productVm;
    }

    public Task<IEnumerable<ProductViewModel>> GetSearchProducts(string keyword)
    {
        return GetProductsFromCacheOrApi(keyword);
    }
    private async Task<IEnumerable<ProductViewModel>> GetProductsFromApi(string keyword)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using var response = await client.GetAsync($"/api/v1/products/search/{keyword}");
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);

            var serializedProducts = JsonSerializer.Serialize(products);
            await _cache.SetStringAsync($"cached_products_{keyword}", serializedProducts, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
            return products;
        }
        else
        {
            throw new ArgumentException("Products not found for the given keyword.");
        }
    }
    private async Task<IEnumerable<ProductViewModel>> GetProductsFromCacheOrApi(string keyword)
    {
        var cachedProducts = await _cache.GetStringAsync($"cached_products_{keyword}");

        if (!string.IsNullOrEmpty(cachedProducts))
        {
            return JsonSerializer.Deserialize<List<ProductViewModel>>(cachedProducts);
        }
        else
        {
            return await GetProductsFromApi(keyword);
        }
    }


}
