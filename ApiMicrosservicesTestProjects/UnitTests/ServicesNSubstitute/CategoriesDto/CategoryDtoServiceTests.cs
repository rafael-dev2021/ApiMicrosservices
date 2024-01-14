using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.Services.Interfaces;
using NSubstitute;

namespace ApiMicrosservicesTestProjects.UnitTests.ServicesNSubstitute.CategoriesDto;
public class CategoryDtoServiceTests
{
    [Fact]
    public async Task GetItemsDtoAsync_ReturnsCategoriesDto()
    {
        // Arrange
        var categoryDtoService = Substitute.For<ICategoryDtoService>();

        var categoryDtos = new List<CategoryDto>
            {
                new() { Id = 1, Name = "Category 1", Image = "Image1.jpg" },
                new() { Id = 2, Name = "Category 2", Image = "Image2.jpg" }
            };

        categoryDtoService.GetItemsDtoAsync().Returns(categoryDtos);

        // Act
        var result = await categoryDtoService.GetItemsDtoAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryDtos.Count, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCategoryDto()
    {
        // Arrange
        var categoryDtoService = Substitute.For<ICategoryDtoService>();

        var categoryId = 1;
        var categoryDto = new CategoryDto { Id = categoryId, Name = "Category 1", Image = "Image1.jpg" };

        categoryDtoService.GetByIdAsync(categoryId).Returns(categoryDto);

        // Act
        var result = await categoryDtoService.GetByIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id);
    }

    [Fact]
    public async Task AddAsync_AddsNewCategory()
    {
        // Arrange
        var categoryDtoService = Substitute.For<ICategoryDtoService>();

        var categoryToAdd = new CategoryDto
        {
            Id = 1,
            Name = "Category 1",
            Image = "Image1.jpg"
        };

        // Act
        await categoryDtoService.AddAsync(categoryToAdd);

        // Assert
        await categoryDtoService.Received().AddAsync(Arg.Any<CategoryDto>());
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingCategory()
    {
        // Arrange
        var categoryDtoService = Substitute.For<ICategoryDtoService>();

        var categoryToUpdate = new CategoryDto
        {
            Id = 1,
            Name = "Category 1",
            Image = "Image1.jpg"
        };

        // Act
        await categoryDtoService.UpdateAsync(categoryToUpdate);

        // Assert
        await categoryDtoService.Received().UpdateAsync(Arg.Any<CategoryDto>());
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingCategory()
    {
        // Arrange
        var categoryDtoService = Substitute.For<ICategoryDtoService>();

        var categoryIdToRemove = 1;

        categoryDtoService.GetByIdAsync(categoryIdToRemove).Returns(new CategoryDto { Id = categoryIdToRemove });

        // Act
        await categoryDtoService.DeleteAsync(categoryIdToRemove);

        // Assert
        await categoryDtoService.Received(1).DeleteAsync(Arg.Any<int>());
    }
}
