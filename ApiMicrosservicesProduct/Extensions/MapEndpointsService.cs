using ApiMicrosservicesProduct.Endpoints;

namespace ApiMicrosservicesProduct.Extensions;

public static class MapEndpointsService
{
    public static void AddMapEndpointsService(this WebApplication app)
    {
        app.MapCategoryServiceEndpoint();
        app.MapProductServiceEndpoint();
    }
}
