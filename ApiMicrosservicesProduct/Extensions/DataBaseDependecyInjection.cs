using ApiMicrosservicesProduct.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesProduct.Extensions;
public static class DataBaseDependecyInjection
{
    public static IServiceCollection AddDataBaseDependecyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });
    }
}