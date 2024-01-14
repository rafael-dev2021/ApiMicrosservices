using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.FluentValidation.ProductDtoValidationLibrary;
using FluentValidation.TestHelper;

namespace TestProjects.UnitTests.ProductDtoValidatorTests;

public class ProductDtoValidatorTest
{
    private readonly ProductDtoValidator _validator;

    public ProductDtoValidatorTest()
    {
        _validator = new ProductDtoValidator();
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
    {
        var product = new ProductDto
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Price = 100,
            Stock = 10
        };
        var result = _validator.TestValidate(product);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Null()
    {
        var product = new ProductDto { Name = null };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Too_Short()
    {
        var product = new ProductDto { Name = "Ab" };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Null()
    {
        var product = new ProductDto { Description = null };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Length_Is_Too_Short()
    {
        var product = new ProductDto { Description = "Short" };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Images_Have_Valid_Length()
    {
        var product = new ProductDto
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Price = 100,
            Stock = 10,
            Images =
        [
            "Image1",
            "Image2",
            "Image3"
        ]
        };
        var result = _validator.TestValidate(product);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Any_Image_Length_Is_Too_Long()
    {
        var stringTest = new string('x', 601);

        var product = new ProductDto
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Price = 100,
            Stock = 10,
            Images =
            [
                "Image1",
                "Image2",
                stringTest
            ]
        };

        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Images);
    }


    [Fact]
    public void Should_Have_Error_When_Price_Is_Outside_Range()
    {
        var product = new ProductDto { Price = 0 };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Price);
    }

    [Fact]
    public void Should_Have_Error_When_Stock_Is_Outside_Range()
    {
        var product = new ProductDto { Stock = 0 };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Stock);
    }
}
