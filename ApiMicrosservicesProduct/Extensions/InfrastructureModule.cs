namespace ApiMicrosservicesProduct.Extensions;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDbContextDependecyInjection(configuration)
            .AddRepositoriesDependecyInjection()
            .AddServicesDependecyInjection()
            .AddExchangeRedisCacheDependecyInjection()
            .AddFluentValidationDependecyInjection();
    }
}

