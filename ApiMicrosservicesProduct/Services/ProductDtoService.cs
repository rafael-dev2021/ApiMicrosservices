using ApiMicrosservicesProduct.DTOs;
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

    public async Task<ProductDto> GetByIdAsync(int? id)
    {
        if (id <= 0 || id == null)
            throw new ArgumentException("Invalid product id. The Id should be a positive integer greater than zero.");

        try
        {
            var getProductId = await _productRepository.GetByIdAsync(id);
            if (getProductId == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Product with ID {id} not found.",
                    Severity = "Not found",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                });
            return _mapper.Map<ProductDto>(getProductId);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing the request.", ex);
        }
    }

    public async Task AddAsync(ProductDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Product cannot be null.");
        try
        {
            var addProduct = _mapper.Map<Product>(entity);
            if (addProduct == null)
                throw new RequestException(new RequestError
                {
                    Message = "Error when adding product.",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _productRepository.CreateAsync(addProduct);
        }
        catch (Exception)
        {
            throw new Exception("Unexpected error occurred while adding the product.");
        }
    }

    public async Task DeleteAsync(int? id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), $"Product {id} cannot be null.");
        try
        {
            var deleteProduct = await _productRepository.GetByIdAsync(id);
            if (deleteProduct == null)
                throw new RequestException(new RequestError
                {
                    Message = "Error when removing product.",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _productRepository.RemoveAsync(deleteProduct);
        }
        catch (Exception)
        {
            throw new Exception("Unexpected error occurred while removing the product."); ;
        }
    }

    public async Task UpdateAsync(ProductDto entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "The product cannot be null.");
        try
        {
            var updateProduct = _mapper.Map<Product>(entity);
            if (updateProduct == null)
                throw new RequestException(new RequestError
                {
                    Message = $"Error when updating the product",
                    Severity = "Error",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            await _productRepository.UpdateAsync(updateProduct);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occurred while updating the product.", ex);
        }
    }
}