using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ApiMicrosservicesTestProjects.IntegrationTests.Repositories.Products;

public class RedisProductCacheDirectAccessTests
{
    private IProductRepository GetRedisRepository()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();
        return new RedisProductRepository(cache);
    }

    private async Task AddProductsToRedis(params Product[] products)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();

        foreach (var product in products)
        {
            var serializedEntity = JsonSerializer.Serialize(product);
            await cache.StringSetAsync($"Product:{product.Id}", serializedEntity);
        }
    }

    private async Task ClearProductFromRedis(int productId)
    {
        using var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var cache = redis.GetDatabase();

        await cache.KeyDeleteAsync($"Product:{productId}");
    }

    private static readonly string[] sourceArray1 = ["Image.jpg"];

    [Fact]
    public async Task GetItemAsync_ReturnProductsFromRedis()
    {
        var productTest1 = new Product(1, "Test 1", "Description 1", 100, 12.00m, [.. sourceArray1], 1);
        var productTest2 = new Product(2, "Test 2", "Description 2", 100, 12.00m, [.. sourceArray1], 2);
        var productTest3 = new Product(3, "Test 3", "Description 3", 100, 12.00m, [.. sourceArray1], 3);

        await AddProductsToRedis(productTest1, productTest2, productTest3);

        var repository = GetRedisRepository();
        var products = await repository.GetItemsAsync();

        Assert.NotNull(products);
        Assert.Equal(2, products.Count());

        await ClearProductFromRedis(productTest1.Id);
        await ClearProductFromRedis(productTest2.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnCorrectProduct()
    {
        var repository = GetRedisRepository();
        var product = await repository.GetByIdAsync(3);

        Assert.NotNull(product);
        product.UpdateNameUnitTest("Test 3");
    }

    [Fact]
    public async Task CreateAsync_AddProduct()
    {
        var repository = GetRedisRepository();
        var productToAdd = new Product(4, "Test 4", "Description 4", 100, 12.00m, [.. sourceArray1], 4);

        await repository.CreateAsync(productToAdd);

        var addedProduct = await repository.GetByIdAsync(4);

        Assert.NotNull(addedProduct);
        Assert.Equal(4, addedProduct.Id);
    }

    [Fact]
    public async Task UpdateAsync_UpdateProduct()
    {
        var repository = GetRedisRepository();

        var existingProduct = await repository.GetByIdAsync(4);

        existingProduct.UpdateProductUnitTest("Update Product", [.. sourceArray1], "Udapte Description", 20.30m, 50, 5);
        var result = await repository.UpdateAsync(existingProduct);

        Assert.NotNull(result);
        Assert.Equal("Update Product", result.Name);
    }

    [Fact]
    public async Task RemoveAsync_RemoveProduct()
    {
        var repository = GetRedisRepository();
        var productToRemove = await repository.GetByIdAsync(4);
        var removedProduct = await repository.RemoveAsync(productToRemove);

        var deletedProduct = await repository.GetByIdAsync(4);

        Assert.NotNull(removedProduct);
        Assert.Null(deletedProduct);
    }

}
