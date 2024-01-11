namespace ApiMicrosservicesProduct.Models.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetSearchProductsAsync(string keyword);
    Task<IEnumerable<Product>> GetProductsByCategoriesAsync(string categoryStr);
}
