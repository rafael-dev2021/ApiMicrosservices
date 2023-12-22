using ApiMicrosservicesProduct.DTOs;
using ApiMicrosservicesProduct.Errors;
using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using ApiMicrosservicesProduct.Services.Interfaces;
using AutoMapper;

namespace ApiMicrosservicesProduct.Services;
public class CategoryDtoService(IMapper mapper, ICategoryRepository categoryRepository) : ICategoryDtoService
{
    private readonly IMapper _mapper = mapper;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<CategoryDto>> GetItemsDtoAsync()
    {
        var categories = await _categoryRepository.GetItemsAsync();
        if (categories?.Any() != true)
        {
            throw new RequestException(new RequestError
            {
                Message = "No categories found.",
                Severity = "Not found",
                StatusCode = System.Net.HttpStatusCode.NotFound
            });
        }
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int? id)
    {
        if (id <= 0 || id == null)
            throw new ArgumentException("Invalid category id. The Id should be a positive integer greater than zero.");

        try
        {
            var getCategoryId = await _categoryRepository.GetByIdAsync(id);
            if (getCategoryId == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Category with ID {id} not found.",
                    Severity = "Not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                });
            return _mapper.Map<CategoryDto>(getCategoryId);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the request.", ex);
        }
    }
    public async Task AddAsync(CategoryDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Category cannot be null.");
        try
        {
            var addCategory = _mapper.Map<Category>(entity);
            if (addCategory == null)
                throw new RequestException(new RequestError
                {
                    Message = "Error when adding category.",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _categoryRepository.CreateAsync(addCategory);
        }
        catch (Exception)
        {
            throw new Exception("Unexpected error occurred while adding the category.");
        }
    }

    public async Task DeleteAsync(int? id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), $"Category {id} cannot be null.");
        try
        {
            var deleteCategory = await _categoryRepository.GetByIdAsync(id);
            if (deleteCategory == null)
                throw new RequestException(new RequestError
                {
                    Message = "Error when removing category.",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _categoryRepository.RemoveAsync(deleteCategory);
        }
        catch (Exception)
        {
            throw new Exception("Unexpected error occurred while removing the category."); ;
        }
    }

    public async Task UpdateAsync(CategoryDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "The category cannot be null.");
        try
        {
            var updateCategory = _mapper.Map<Category>(entity);
            if (updateCategory == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Error when updating the category",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _categoryRepository.UpdateAsync(updateCategory);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while updating the category.", ex);
        }
    }
}