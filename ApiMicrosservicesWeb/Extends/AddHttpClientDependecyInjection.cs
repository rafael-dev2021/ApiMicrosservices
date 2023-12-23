using ApiMicrosservicesWeb.Services.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;

namespace ApiMicrosservicesWeb.Extends;

public static class AddHttpClientDependecyInjection
{
    public static IServiceCollection AddHttpClientDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IProductService, ProductService>("ProductApi", c =>
        {
            c.BaseAddress = new Uri(configuration["ServiceUri:ProductApi"]);
            c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-ProductApi");
        });
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}
