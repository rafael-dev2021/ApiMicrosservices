using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiMicrosservicesProduct.Endpoints
{
    public static class CategoryServiceEndpoint
    {
        private static async Task<T> GetCachedData<T>(
            IDistributedCache cache, 
            string key)
        {
            var cachedData = await cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedData))
                return JsonConvert.DeserializeObject<T>(cachedData);

            return default;
        }

        private static async Task SetCachedData<T>(
            IDistributedCache cache,
            string key, 
            T data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            await cache.SetStringAsync(key, serializedData,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600)
                });
        }
        public static void MapCategoryServiceEndpoint(this WebApplication app)
        {
            app.MapGet("/api/v1/categories", async (
                [FromServices] ICategoryDtoService service,
                IDistributedCache cache) =>
            {
                var cachedCategories = await GetCachedData<List<CategoryDto>>(cache, "cached_categories");
                if (cachedCategories != null) return Results.Ok(cachedCategories);

                var categories = await service.GetItemsDtoAsync();
                if (categories == null || !categories.Any()) return Results.NotFound("No categories found.");

                await SetCachedData(cache, "cached_categories", categories);
                return Results.Ok(categories);
            });


            app.MapGet("/api/v1/categories/{id}", async (
                [FromServices] ICategoryDtoService service,
                IDistributedCache cache,
                int? id) =>
            {
                var cachedCategory = await GetCachedData<CategoryDto>(cache, $"cached_category_{id}");
                if (cachedCategory != null) return Results.Ok(cachedCategory);

                var category = await service.GetByIdAsync(id);
                if (category == null) return Results.NotFound("Category not found.");

                await SetCachedData(cache, $"cached_category_{id}", category);
                return Results.Ok(category);
            });

            app.MapPost("/api/v1/categories", async (
                [FromServices] ICategoryDtoService service, 
                IDistributedCache cache,
                [FromBody] CategoryDto categoryDto,
                [FromServices]IValidator<CategoryDto> validator) =>
            {

                var validationResult = await validator.ValidateAsync(categoryDto);
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);

                if (!validationResult.IsValid) return Results.BadRequest(errors);
                try
                {
                    await service.AddAsync(categoryDto);
                    await cache.RemoveAsync("cached_categories");
                    return Results.Created($"/api/v1/addcategory/{categoryDto.Id}", categoryDto);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest("An error ocurred while adding the category:" + ex.Message);
                }
            });

            app.MapPut("/api/v1/categories{id}", async (
                int? id,
                [FromServices] ICategoryDtoService service, 
                IDistributedCache cache,
                [FromBody] CategoryDto categoryDto,
                [FromServices] IValidator<CategoryDto> validator) =>
            {
                if (id == null) return Results.BadRequest("Invalid category data.");
                var validationResult = await validator.ValidateAsync(categoryDto);
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);

                if (!validationResult.IsValid) return Results.BadRequest(errors);

                try
                {
                    await service.UpdateAsync(categoryDto);
                    await cache.RemoveAsync($"cached_categories_{id}");
                    await cache.RemoveAsync("cached_categories");
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest("An error ocurred while updating the category: " + ex.Message);
                }
            });

            app.MapDelete("/api/v1/categories/{id}", async (
                ICategoryDtoService service,
                IDistributedCache cache,
                int? id) =>
            {
                if (id == null) return Results.NotFound($"Category with {id} not found.");
                var category = await service.GetByIdAsync(id);
                if (category == null) return Results.NotFound($"Category with {id} not found.");

                await service.DeleteAsync(id.Value);
                await cache.RemoveAsync($"cached_categories_{id}");
                await cache.RemoveAsync("cached_categories");
                return Results.NoContent();
            });

        }
    }
}
