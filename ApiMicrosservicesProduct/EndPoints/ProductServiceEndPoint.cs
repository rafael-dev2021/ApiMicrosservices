using ApiMicrosservicesProduct.DTOs;
using ApiMicrosservicesProduct.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMicrosservicesProduct.EndPoints;

[Authorize]
public static class ProductServiceEndPoint
{
    public static void MapProductServiceEndpoints(this WebApplication app)
    {
        app.MapGet("/api/products", async ([FromServices] IProductDtoService service) =>
        {
            return await service.GetItemsDtoAsync();
        });

        app.MapGet("/api/products/{id}", async ([FromServices] IProductDtoService service, int? id) =>
        {
            var product = await service.GetByIdAsync(id);
            if (product == null) TypedResults.NotFound();

            return product;
        });

        app.MapPost("/api/products", [Authorize(Roles = "Admin")] async ([FromServices] IProductDtoService service, [FromBody] ProductDto productDto) =>
        {
            await service.AddAsync(productDto);
            return TypedResults.Created($"/api/products/{productDto.Id}", productDto);
        });


        app.MapPut("/api/products/{id}", [Authorize(Roles = "Admin")] async ([FromServices] IProductDtoService service, int? id, [FromBody] ProductDto updateProductDto) =>
        {
            if (id != updateProductDto.Id) TypedResults.BadRequest();

            if (updateProductDto == null) TypedResults.BadRequest();

            await service.UpdateAsync(updateProductDto);

            return TypedResults.Ok();
        });


        app.MapDelete("/api/products/{id}", [Authorize(Roles = "Admin")] async ([FromServices] IProductDtoService service, int? id) =>
        {
            if (id == null) TypedResults.NotFound();

            var product = await service.GetByIdAsync(id);
            if (product == null) TypedResults.NotFound();

            await service.DeleteAsync(id.Value);
            return TypedResults.NoContent();
        });
    }
}