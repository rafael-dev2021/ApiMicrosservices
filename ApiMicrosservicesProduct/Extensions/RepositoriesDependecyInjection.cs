using ApiMicrosservicesProduct.Context.Repositories;
using ApiMicrosservicesProduct.Models.Interfaces;

namespace ApiMicrosservicesProduct.Extensions;

public static class RepositoriesDependecyInjection
{
    public static IServiceCollection AddRepositoriesDependecyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepositories>();
        services.AddScoped<IProductRepository, ProductRepositories>();

        return services;
    }
}
