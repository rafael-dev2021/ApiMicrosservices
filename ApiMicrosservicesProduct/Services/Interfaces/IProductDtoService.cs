using ApiMicrosservicesProduct.Dtos;

namespace ApiMicrosservicesProduct.Services.Interfaces;

public interface IProductDtoService : IGenericService<ProductDto>
{
    Task<IEnumerable<ProductDto>> GetSearchProductsDtoAsync(string keyword);
    Task<IEnumerable<ProductDto>> GetProductsByCategoriesDtoAsync(string categoryStr);
}
