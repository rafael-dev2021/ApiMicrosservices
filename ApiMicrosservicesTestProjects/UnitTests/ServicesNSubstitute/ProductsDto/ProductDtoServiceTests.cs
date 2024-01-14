using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.Services.Interfaces;
using NSubstitute;

namespace ApiMicrosservicesTestProjects.UnitTests.ServicesNSubstitute.ProductsDto;

public class ProductDtoServiceTests
{
    private static readonly string[] sourceArray1 = ["Image1.jpg"];
    private static readonly string[] sourceArray2 = ["Image2.jpg"];

    [Fact]
    public async Task GetItemsDtoAsync_ReturnsProductsDto()
    {
        // Arrange
        var productDtoService = Substitute.For<IProductDtoService>();

        var productDtos = new List<ProductDto>
        {
            new() { Id = 1, Name = "Product 1", Images = [.. sourceArray1], Description = "Description 1", Price = 10.0m, Stock = 100, CategoryId = 1 },
            new() { Id = 2, Name = "Product 2", Images = [.. sourceArray2], Description = "Description 2", Price = 20.0m, Stock = 50, CategoryId = 2 }
        };

        productDtoService.GetItemsDtoAsync().Returns(productDtos);

        // Act
        var result = await productDtoService.GetItemsDtoAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDtos.Count, result.Count());
    }



    [Fact]
    public async Task GetByIdAsync_ReturnsProductDto()
    {
        // Arrange
        var productDtoService = Substitute.For<IProductDtoService>();

        var productId = 1;
        var productDto = new ProductDto { Id = productId, Name = "Product 1", Images = [.. sourceArray1], Description = "Description 1", Price = 10.0m, Stock = 10, CategoryId = 1 };

        productDtoService.GetByIdAsync(productId).Returns(productDto);

        // Act
        var result = await productDtoService.GetByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
    }




    [Fact]
    public async Task AddAsync_AddsNewProduct()
    {
        // Arrange
        var productDtoService = Substitute.For<IProductDtoService>(); 

        var productToAdd = new ProductDto
        {
            Id = 1,
            Name = "Product 1",
            Images = ["Image1.jpg"],
            Description = "Description 1",
            Price = 10.0m,
            Stock = 10,
            CategoryId = 1
        };

        // Act
        await productDtoService.AddAsync(productToAdd);

        // Assert
        await productDtoService.Received().AddAsync(Arg.Any<ProductDto>());
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingProduct()
    {
        // Arrange
        var productDtoService = Substitute.For<IProductDtoService>();

        var productToUpdate = new ProductDto
        {
            Id = 1,
            Name = "Product 1",
            Images = ["Image1.jpg"],
            Description = "Description 1",
            Price = 10.0m,
            Stock = 10,
            CategoryId = 1
        };

        // Act
        await productDtoService.UpdateAsync(productToUpdate);

        // Assert
        await productDtoService.Received().UpdateAsync(Arg.Any<ProductDto>());
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingProduct()
    {
        // Arrange
        var productDtoService = Substitute.For<IProductDtoService>();

        var productIdToRemove = 1;

        productDtoService.GetByIdAsync(productIdToRemove).Returns(new ProductDto { Id = productIdToRemove });

        // Act
        await productDtoService.DeleteAsync(productIdToRemove);

        // Assert
        await productDtoService.Received(1).DeleteAsync(Arg.Any<int>());
    }
}
