namespace ApiMicrosservicesProduct.Models.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetItemsAsync();
    Task<T> GetByIdAsync(int? id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> RemoveAsync(T entity);
}