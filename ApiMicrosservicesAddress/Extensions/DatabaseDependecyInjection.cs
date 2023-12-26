using ApiMicrosservicesAddress.Context;
using ApiMicrosservicesAddress.Providers;
using ApiMicrosservicesAddress.Providers.Interfaces;
using ApiMicrosservicesAddress.Services;
using ApiMicrosservicesAddress.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesAddress.Extensions;

public static class DatabaseDependecyInjection
{
    public static IServiceCollection AddDatabaseDependecyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });
        services.AddScoped<IAddressService, AddressService>();
        services.AddSingleton<IHttpClient, HttpClientMicrosoft>();

        return services;
    }
}
