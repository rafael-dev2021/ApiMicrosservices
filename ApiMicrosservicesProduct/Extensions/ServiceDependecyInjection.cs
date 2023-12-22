using ApiMicrosservicesProduct.DTOs.Mappings;
using ApiMicrosservicesProduct.Services;
using ApiMicrosservicesProduct.Services.Interfaces;

namespace ApiMicrosservicesProduct.Extensions;
public static class ServiceDependecyInjection
{
    public static IServiceCollection AddServiceDependecyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICategoryDtoService, CategoryDtoService>();
        services.AddScoped<IProductDtoService, ProductDtoService>();

        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}