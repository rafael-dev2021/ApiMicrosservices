using ApiMicrosservicesWeb.Models.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace ApiMicrosservicesWeb.Services.MicrosservicesProduct;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/products/";
    private const string apiEndpointId = "/api/products/{id}";
    private ProductViewModel productVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);


        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var productsViewModels = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);

                return productsViewModels ?? Enumerable.Empty<ProductViewModel>();
            }
            else
            {
                return Enumerable.Empty<ProductViewModel>();
            }
        }
    }


    public async Task<ProductViewModel> GetByProductIdAsync(int? id, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);


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

    public async Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);


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

    public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);


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


    public async Task<bool> DeleteProductAsync(int? id, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHeaderAuthorization(token, client);


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
