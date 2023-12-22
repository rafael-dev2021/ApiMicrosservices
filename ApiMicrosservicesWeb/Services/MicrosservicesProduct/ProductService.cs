using ApiMicrosservicesWeb.Models.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
using System.Text.Json;
using System.Text;

namespace ApiMicrosservicesWeb.Services.MicrosservicesProduct;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/products/";
    private const string apiEndpointId = "/api/products/{id}";
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsViewModels;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productsViewModels = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return productsViewModels;
    }

    public async Task<ProductViewModel> GetByProductIdAsync(int? id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpointId.Replace("{id}", id.ToString())))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
        return productVM;
    }

    public async Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        StringContent content = new(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
        return productViewModel;
    }

    public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        ProductViewModel productUpdated = null;

        using (var response = await client.PutAsJsonAsync(apiEndpointId.Replace("{id}", productViewModel.Id.ToString()), productViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    productUpdated = JsonSerializer.Deserialize<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
        return productUpdated;
    }


    public async Task<bool> DeleteProductAsync(int? id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        if (id.HasValue)
        {
            string deleteUrl = $"{apiEndpoint}{id}";

            using var response = await client.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                _ = await response.Content.ReadAsStreamAsync();
                return true;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
        else
        {
            return false;
        }
    }
}
