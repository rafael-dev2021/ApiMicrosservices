using ApiMicrosservicesWeb.Services;
using ApiMicrosservicesWeb.Services.Interfaces;

namespace ApiMicrosservicesWeb.Extensions;

public static class ViewModelService
{
    public static IServiceCollection AddViewModelService(this IServiceCollection services)
    {
        services.AddScoped<IProductViewModelService, ProductViewModelService>();

        return services;
    }
}
