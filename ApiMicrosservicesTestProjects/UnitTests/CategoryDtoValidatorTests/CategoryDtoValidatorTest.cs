using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.FluentValidation.CategoryDtoValidationLibrary;
using FluentValidation.TestHelper;

namespace TestProjects.UnitTests.CategoryDtoValidatorTests;

public class CategoryDtoValidatorTest
{
    private readonly CategoryDtoValidator _validator;

    public CategoryDtoValidatorTest()
    {
        _validator = new CategoryDtoValidator();
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
    {
        var category = new CategoryDto
        {
            Name = "Valid Name",
            Image = "Valid Image"
        };
        var result = _validator.TestValidate(category);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Null()
    {
        var category = new CategoryDto { Name = null };
        var result = _validator.TestValidate(category);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Too_Short()
    {
        var category = new CategoryDto { Name = "Ab" };
        var result = _validator.TestValidate(category);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Image_Is_Null()
    {
        var category = new CategoryDto { Image = null };
        var result = _validator.TestValidate(category);
        result.ShouldHaveValidationErrorFor(c => c.Image);
    }

    [Fact]
    public void Should_Have_Error_When_Image_Length_Is_Too_Long()
    {
        var stringTest = new string('x', 601);

        var category = new CategoryDto { Image = stringTest };
        var result = _validator.TestValidate(category);
        result.ShouldHaveValidationErrorFor(c => c.Image);
    }
}
