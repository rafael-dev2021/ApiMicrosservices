using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ApiMicrosservicesTestProjects.IntegrationTests.Repositories.Products;

public class RedisProductRepository(IDatabase cache) : IProductRepository
{
    private readonly IDatabase _cache = cache;

    public async Task<IEnumerable<Product>> GetItemsAsync()
    {
        var products = new List<Product>();

        for (int i = 1; i <= 2; i++)
        {
            var product = await _cache.StringGetAsync($"Product:{i}");
            if (!product.IsNullOrEmpty)
            {
                var productObject = JsonSerializer.Deserialize<Product>(product);
                products.Add(productObject);
            }
        }

        return products;
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        if (id.HasValue)
        {
            var product = await _cache.StringGetAsync($"Product:{id}");
            if (!product.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<Product>(product);
            }
        }
        return null;
    }
    public async Task<Product> CreateAsync(Product entity)
    {
        var serializedEntity = JsonSerializer.Serialize(entity);
        await _cache.StringSetAsync($"Product:{entity.Id}", serializedEntity);
        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        var product = await GetByIdAsync(entity.Id);
        if (product != null)
        {
            var serializedEntity = JsonSerializer.Serialize(entity);
            await _cache.StringSetAsync($"Product:{entity.Id}", serializedEntity);
            return entity;
        }
        return null;
    }


    public async Task<Product> RemoveAsync(Product entity)
    {
        var product = await GetByIdAsync(entity.Id);
        if(product != null)
        {
            await _cache.KeyDeleteAsync($"Product:{entity.Id}");
        }
        return product;
    }





    public Task<IEnumerable<Product>> GetProductsByCategoriesAsync(string categoryStr)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetSearchProductsAsync(string keyword)
    {
        throw new NotImplementedException();
    }
}
