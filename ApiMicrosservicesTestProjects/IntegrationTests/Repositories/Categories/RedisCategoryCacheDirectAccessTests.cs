using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using NSubstitute.Exceptions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiMicrosservicesTestProjects.IntegrationTests.Repositories.Categories;

public class RedisCategoryCacheDirectAccessTests
{
    private ICategoryRepository GetRedisRepository()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();
        return new RedisCategoryRepository(cache);
    }

    private async Task AddCategoriesToRedis(params Category[] categories)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();

        foreach (var category in categories)
        {
            var serializedEntity = JsonSerializer.Serialize(category);
            await cache.StringSetAsync($"Category:{category.Id}", serializedEntity);
        }
    }

    private async Task ClearCategoryFromRedis(int categoryId)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();

        await cache.KeyDeleteAsync($"Category:{categoryId}");
    }

    [Fact]
    public async Task GetITemsAsync_ReturnCategoriesFromRedis()
    {
        var categoryTest1 = new Category(1, "Test 1", "Image1.jpg");
        var categoryTest2 = new Category(2, "Test 2", "Image2.jpg");
        var categoryTest3 = new Category(3, "Test 3", "Image3.jpg");

        await AddCategoriesToRedis(categoryTest1, categoryTest2,categoryTest3);

        var repository = GetRedisRepository();
        var categories = await repository.GetItemsAsync();

        Assert.NotNull(categories);
        Assert.Equal(2, categories.Count());

        await ClearCategoryFromRedis(categoryTest1.Id);
        await ClearCategoryFromRedis(categoryTest2.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnCorrectCategory()
    {
        var repository = GetRedisRepository();
        var category = await repository.GetByIdAsync(3);

        Assert.NotNull(category);
        Assert.Equal("Update Test 3", category.Name);
    }

    [Fact]
    public async Task CreateAsync_AddCategory()
    {
        var repository = GetRedisRepository();
        var categoryToAdd = new Category(4, "Test 4", "Image4.jpg");

        await repository.CreateAsync(categoryToAdd);

        var addedCategory = await repository.GetByIdAsync(4);

        Assert.NotNull(addedCategory);
        Assert.Equal(4, addedCategory.Id);
    }

    [Fact]
    public async Task UpdateAsync_UpdateCategory()
    {
        var repository = GetRedisRepository();
        var categoryToUpdate = await repository.GetByIdAsync(3);
        var updatedCategory = new Category(categoryToUpdate.Id, "Update Test 3", categoryToUpdate.Image);

        await repository.UpdateAsync(updatedCategory);

        var retrievedUpdateCategory = await repository.GetByIdAsync(3);

        Assert.NotNull(retrievedUpdateCategory);
        Assert.Equal("Update Test 3", retrievedUpdateCategory.Name);
    }

    [Fact]
    public async Task RemoveAsync_RemoveCategory()
    {
        var repository = GetRedisRepository();
        var categoryToRemove = await repository.GetByIdAsync(4);
        var removedCategory = await repository.RemoveAsync(categoryToRemove);

        var deletedCategory = await repository.GetByIdAsync(4);

        Assert.NotNull(removedCategory);
        Assert.Null(deletedCategory);
    }
}
