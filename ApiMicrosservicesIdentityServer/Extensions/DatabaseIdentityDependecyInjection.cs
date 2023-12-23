using ApiMicrosservicesIdentityServer.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesIdentityServer.Extensions;

public static class DatabaseIdentityDependecyInjection
{
    public static IServiceCollection AddDatabaseIdentityDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContextIdentity>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AppDbContextIdentity).Assembly.FullName));
        });

        return services;
    }
}
