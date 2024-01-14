using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using StackExchange.Redis;
using System.Text.Json;


namespace ApiMicrosservicesTestProjects.IntegrationTests.Repositories.Categories;

public class RedisCategoryRepository(IDatabase cache) : ICategoryRepository
{
    private readonly IDatabase _cache = cache;


    public async Task<IEnumerable<Category>> GetItemsAsync()
    {
        var categories = new List<Category>();

        for (int i = 1; i <= 2; i++)
        {
            var category = await _cache.StringGetAsync($"Category:{i}");
            if (!category.IsNull)
            {
                var categoryObject = JsonSerializer.Deserialize<Category>(category);
                categories.Add(categoryObject);
            }
        }

        return categories;
    }

    public async Task<Category> GetByIdAsync(int? id)
    {
        if (id.HasValue)
        {
            var category = await _cache.StringGetAsync($"Category:{id}");
            if (!category.IsNullOrEmpty) 
            {
                return JsonSerializer.Deserialize<Category>(category);
            }
        }
        return null;
    }
    public async Task<Category> CreateAsync(Category entity)
    {
        var serializedEntity = JsonSerializer.Serialize(entity);
        await _cache.StringSetAsync($"Category:{entity.Id}", serializedEntity);
        return entity;
    }

    public async Task<Category> RemoveAsync(Category entity)
    {
        var category = await GetByIdAsync(entity.Id);
        if(category != null) 
        {
            await _cache.KeyDeleteAsync($"Category:{entity.Id}");
        }
        return category;
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        var category = await GetByIdAsync(entity.Id);
        if(category != null)
        {
            var serializedEntity = JsonSerializer.Serialize(entity);
            await _cache.StringSetAsync($"Category:{entity.Id}", serializedEntity);
            return entity;
        }
        return null;
    }
}
