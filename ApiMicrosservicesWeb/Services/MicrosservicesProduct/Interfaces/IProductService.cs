using ApiMicrosservicesWeb.Models.MicrosservicesProduct;

namespace ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
    Task<ProductViewModel> GetByProductIdAsync(int? id, string token);
    Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel, string token);
    Task<ProductViewModel> UpdateProductAsync(ProductViewModel productViewModel, string token);
    Task<bool> DeleteProductAsync(int? id, string token);
}
