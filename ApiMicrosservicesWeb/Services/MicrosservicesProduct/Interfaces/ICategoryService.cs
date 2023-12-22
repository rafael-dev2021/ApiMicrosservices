using ApiMicrosservicesWeb.Models.MicrosservicesProduct;

namespace ApiMicrosservicesWeb.Services.MicrosservicesProduct.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories();
}
