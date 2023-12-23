using ApiMicrosservicesProduct.DTOs;
using ApiMicrosservicesProduct.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMicrosservicesProduct.EndPoints;
public static class CategoryServiceEndPoint
{
    public static void MapCategoryServiceEndpoints(this WebApplication app)
    {
        app.MapGet("/api/categories", async ([FromServices] ICategoryDtoService service) =>
        {
            return await service.GetItemsDtoAsync();
        });

        app.MapGet("/api/categories/{id}", async ([FromServices] ICategoryDtoService service, int? id) =>
        {
            var category = await service.GetByIdAsync(id);
            if (category == null) TypedResults.NotFound();

            return category;
        });

        app.MapPost("/api/categories", [Authorize(Roles = "Admin")] async ([FromServices] ICategoryDtoService service, [FromBody] CategoryDto categoryDto) =>
        {
            await service.AddAsync(categoryDto);
            return TypedResults.Created($"/api/categories/{categoryDto.Id}", categoryDto);
        });


        app.MapPut("/api/categories/{id}", [Authorize(Roles = "Admin")] async ([FromServices] ICategoryDtoService service, int? id, [FromBody] CategoryDto updateCategoryDto) =>
        {
            if (id != updateCategoryDto.Id) TypedResults.BadRequest();

            if (updateCategoryDto == null) TypedResults.BadRequest();

            await service.UpdateAsync(updateCategoryDto);

            return TypedResults.Ok();
        });

        app.MapDelete("/api/categories/{id}", [Authorize(Roles = "Admin")] async (int? id, [FromServices] ICategoryDtoService service) =>
        {
            if (id == null) return Results.NotFound();

            var smartphone = await service.GetByIdAsync(id);
            if (smartphone == null) return Results.NotFound();

            await service.DeleteAsync(id.Value);
            return TypedResults.NoContent();
        });
    }
}