using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.FluentValidation.CategoryDtoValidationLibrary;
using ApiMicrosservicesProduct.FluentValidation.ProductDtoValidationLibrary;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ApiMicrosservicesProduct.Extensions;

public static class FluentValidationDependecyInjection
{
    public static IServiceCollection AddFluentValidationDependecyInjection(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CategoryDtoValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
        services.AddScoped<IValidator<CategoryDto>, CategoryDtoValidator>();
        return services;
    }
}
