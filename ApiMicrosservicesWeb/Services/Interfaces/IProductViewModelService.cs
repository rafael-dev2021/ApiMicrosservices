using ApiMicrosservicesWeb.Models.MicrosserviceProduct;

namespace ApiMicrosservicesWeb.Services.Interfaces;

public interface IProductViewModelService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<IEnumerable<ProductViewModel>> GetSearchProducts(string keyword);
    Task<ProductViewModel> GetbyIdAsync(int? id);
    Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel);
    Task<bool> DeleteAsync(int? id);
    Task<ProductViewModel> UpdateAsync(ProductViewModel productViewModel);

}
