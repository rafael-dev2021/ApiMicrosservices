using ApiMicrosservicesProduct.Dtos;
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
        var getCategories = await _categoryRepository.GetItemsAsync();
        if (getCategories == null || !getCategories.Any())
        {
            return Enumerable.Empty<CategoryDto>();
        }
        return _mapper.Map<IEnumerable<CategoryDto>>(getCategories);
    }

    public async Task<CategoryDto> GetByIdAsync(int? id)
    {
        if (id <= 0 || id == null)
            throw new ArgumentException("Invalid category id. The id should be a positive integer greater than zero.");

        try
        {
            var getCategoryId = await _categoryRepository.GetByIdAsync(id);
            if (getCategoryId == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Category with {id} not found.",
                    Severity = "Not found.",
                    HttpStatus = System.Net.HttpStatusCode.NotFound
                });
            return _mapper.Map<CategoryDto>(getCategoryId);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error ocurred while processing the request.", ex);
        }
    }

    public async Task AddAsync(CategoryDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Category not found.");

        try
        {
            var addCategory= _mapper.Map<Category>(entity);
            if (addCategory == null)
                throw new RequestException(new RequestError
                {
                    Message = "Erro when adding category.",
                    Severity = "Error.",
                    HttpStatus = System.Net.HttpStatusCode.BadRequest
                });
            await _categoryRepository.CreateAsync(addCategory);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error ocurred while adding the category", ex);
        }
    }

    public async Task DeleteAsync(int? id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), "Category id not found.");

        var getCategoryId = await _categoryRepository.GetByIdAsync(id);
        if (getCategoryId == null)
            throw new RequestException(new RequestError
            {
                Message = $"Category with {id} not found",
                Severity = "Not found",
                HttpStatus = System.Net.HttpStatusCode.NotFound
            });
        var removeCategory= await _categoryRepository.RemoveAsync(getCategoryId);

        _mapper.Map<CategoryDto>(removeCategory);
    }

    public async Task UpdateAsync(CategoryDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Category not found.");

        try
        {
            var updateCategory= _mapper.Map<Category>(entity);
            if (updateCategory == null)
                throw new RequestException(new RequestError
                {
                    Message = "Erro when updating category.",
                    Severity = "Error.",
                    HttpStatus = System.Net.HttpStatusCode.BadRequest
                });
            await _categoryRepository.UpdateAsync(updateCategory);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error ocurred while updating the category", ex);
        }
    }
}

