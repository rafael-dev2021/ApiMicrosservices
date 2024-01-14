using ApiMicrosservicesProduct.Dtos;
using FluentValidation;

namespace ApiMicrosservicesProduct.FluentValidation.CategoryDtoValidationLibrary;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("Category name is required")
            .Length(3, 50).WithMessage("Name should have between 3 and 100 characters");

        RuleFor(category => category.Image)
            .NotEmpty().WithMessage("Image is required")
            .MaximumLength(600).WithMessage("Image should have a maximum length of 600 characters");
    }
}
