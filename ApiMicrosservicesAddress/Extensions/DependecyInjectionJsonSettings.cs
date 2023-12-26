namespace ApiMicrosservicesAddress.Extensions;

public static class DependecyInjectionJsonSettings
{
    public static IServiceCollection AddJsonSettings(this IServiceCollection services)
    {

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddControllers()
            .AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNamingPolicy = null);

        return services;
    }
}
