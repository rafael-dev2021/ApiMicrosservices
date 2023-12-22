using ApiMicrosservicesWeb.Models.MicrosservicesProduct;

namespace ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> GetByProductIdAsync(int? id);
    Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel);
    Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel);
    Task<bool> DeleteProductAsync(int? id);
}
