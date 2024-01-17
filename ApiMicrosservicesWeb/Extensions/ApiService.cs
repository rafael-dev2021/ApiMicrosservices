using ApiMicrosservicesWeb.Services;
using ApiMicrosservicesWeb.Services.Interfaces;

namespace ApiMicrosservicesWeb.Extensions;

public static class ApiService
{
    public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IProductViewModelService, ProductViewModelService>("ProductApi", c =>
        {
            c.BaseAddress = new Uri(configuration["ServiceUri:ProductApi"]);
        });

        return services;
    }
}
