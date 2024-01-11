using ApiMicrosservicesProduct.Dtos;
using ApiMicrosservicesProduct.Errors;
using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using ApiMicrosservicesProduct.Services.Interfaces;
using AutoMapper;

namespace ApiMicrosservicesProduct.Services;

public class ProductDtoService(IMapper mapper, IProductRepository productRepository) : IProductDtoService
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<ProductDto>> GetItemsDtoAsync()
    {
        var getProducts = await _productRepository.GetItemsAsync();
        if (getProducts == null || !getProducts.Any())
        {
            return Enumerable.Empty<ProductDto>();
        }
        return _mapper.Map<IEnumerable<ProductDto>>(getProducts);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoriesDtoAsync(string categoryStr)
    {
        var getProductByCategory = await _productRepository.GetProductsByCategoriesAsync(categoryStr);
        return _mapper.Map<IEnumerable<ProductDto>>(getProductByCategory);

    }

    public async Task<IEnumerable<ProductDto>> GetSearchProductsDtoAsync(string keyword)
    {
        var getSearchProduct = await _productRepository.GetSearchProductsAsync(keyword);
        return _mapper.Map<IEnumerable<ProductDto>>(getSearchProduct);
    }
    public async Task<ProductDto> GetByIdAsync(int? id)
    {
        if (id <= 0 || id == null)
            throw new ArgumentException("Invalid product id. The id should be a positive integer greater than zero.");

        try
        {
            var getProductId = await _productRepository.GetByIdAsync(id);
            if (getProductId == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Product with {id} not found.",
                    Severity = "Not found.",
                    HttpStatus = System.Net.HttpStatusCode.NotFound
                });
            return _mapper.Map<ProductDto>(getProductId);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error ocurred while processing the request.", ex);
        }
    }
    public async Task AddAsync(ProductDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Product not found.");

        try
        {
            var addProduct = _mapper.Map<Product>(entity);
            if (addProduct == null)
                throw new RequestException(new RequestError
                {
                    Message = "Erro when adding produt.",
                    Severity = "Error.",
                    HttpStatus = System.Net.HttpStatusCode.BadRequest
                });
            await _productRepository.CreateAsync(addProduct);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error ocurred while adding the product", ex);
        }
    }

    public async Task DeleteAsync(int? id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), "Product id not found.");

        var getProductId = await _productRepository.GetByIdAsync(id);
        if (getProductId == null)
            throw new RequestException(new RequestError
            {
                Message = $"Product with {id} not found",
                Severity = "Not found",
                HttpStatus = System.Net.HttpStatusCode.NotFound
            });
        var removeProduct = await _productRepository.RemoveAsync(getProductId);

        _mapper.Map<ProductDto>(removeProduct);
    }

    public async Task UpdateAsync(ProductDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Product not found.");

        try
        {
            var updateProduct = _mapper.Map<Product>(entity);
            if (updateProduct == null)
                throw new RequestException(new RequestError
                {
                    Message = "Erro when updating produt.",
                    Severity = "Error.",
                    HttpStatus = System.Net.HttpStatusCode.BadRequest
                });
            await _productRepository.UpdateAsync(updateProduct);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error ocurred while updating the product", ex);
        }
    }
}
