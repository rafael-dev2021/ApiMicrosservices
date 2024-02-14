using ApiMicrosservicesWeb.Services.MicrosservicesAdress;
using ApiMicrosservicesWeb.Services.MicrosservicesAdress.Interfaces;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct;
using ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
using ApiMicrosservicesWeb.Services.MicrosservicesShoppingCart.Interfaces;
using ApiMicrosservicesWeb.Services.MicrosservicesShoppingCart;
using ApiMicrosservicesWeb.Models;

namespace ApiMicrosservicesWeb.Extends;

public static class AddHttpClientDependecyInjection
{
    public static IServiceCollection AddHttpClientDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IAddressService, AddressDtoService>("AddressApi", c =>
        {
            c.BaseAddress = new Uri(configuration["ServiceUri:AddressApi"]);
            c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-AddressApi");
        });

        services.AddHttpClient<IProductService, ProductService>("ProductApi", c =>
        {
            c.BaseAddress = new Uri(configuration["ServiceUri:ProductApi"]);
            c.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-ProductApi");
        });

        services.AddHttpClient<ICartService, CartService>("CartApi",
         c => c.BaseAddress = new Uri(configuration["ServiceUri:CartApi"]));

        services.AddHttpClient<ICouponService, CouponService>("DiscountApi", c =>
        c.BaseAddress = new Uri(configuration["ServiceUri:DiscountApi"]));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAddressService, AddressDtoService>();

        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}
