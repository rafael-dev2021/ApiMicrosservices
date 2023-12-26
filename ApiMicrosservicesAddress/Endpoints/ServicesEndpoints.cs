using ApiMicrosservicesAddress.Errors;
using ApiMicrosservicesAddress.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiMicrosservicesAddress.Endpoints;

public static class ServicesEndPoints
{
    public static void MapAddressServiceEndpoints(this WebApplication app)
    {
        app.MapGet("/api/address/{cep}", async Task<IResult> (string cep,[FromServices] IAddressService service) =>
        {
            try
            {
                var foundAddressInDb = await service.GetCepAsync(cep);
                return TypedResults.Ok(foundAddressInDb);
            }
            catch (RequestException ex)
            {
                return ex.StatusCode switch
                {
                    HttpStatusCode.NotFound => Results.NotFound(new
                    {
                        ex.Message,
                        ex.StatusCode,
                        severity = ex.Severity
                    }),
                    _ => Results.BadRequest(ex.Message),
                };
            }
        });

        app.MapGet("/api/address", async ([FromServices] IAddressService  service) =>
        {
            try
            {
                var foundAddressInDb = await service.GetAllAddressAsync();
                return Results.Ok(foundAddressInDb);
            }
            catch (RequestException ex)
            {
                return ex.StatusCode switch
                {
                    HttpStatusCode.NotFound => Results.NotFound(new
                    {
                        ex.Message,
                        ex.StatusCode,
                        severity = ex.Severity
                    }),
                    _ => Results.BadRequest(ex.Message),
                };
            }
        });
    }
}
