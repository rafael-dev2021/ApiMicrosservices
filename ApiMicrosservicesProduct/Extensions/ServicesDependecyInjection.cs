using ApiMicrosservicesProduct.Dtos.Mappings;
using ApiMicrosservicesProduct.Services;
using ApiMicrosservicesProduct.Services.Interfaces;

namespace ApiMicrosservicesProduct.Extensions;

public static class ServicesDependecyInjection
{
    public static IServiceCollection AddServicesDependecyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICategoryDtoService, CategoryDtoService>();
        services.AddScoped<IProductDtoService, ProductDtoService>();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}

