namespace ApiMicrosservicesWeb.Extensions;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApiService(configuration)
            .AddViewModelService();

        return services;
    }
}
