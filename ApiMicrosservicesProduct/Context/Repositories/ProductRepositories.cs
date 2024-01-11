using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesProduct.Context.Repositories;

public class ProductRepositories(AppDbContext appDbContext) : IProductRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Product>> GetItemsAsync()
    {
        return await _appDbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoriesAsync(string categoryStr)
    {
        return await _appDbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.Category.Name.Equals(categoryStr))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetSearchProductsAsync(string keyword)
    {
        var products = await _appDbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .ToListAsync();

        var filteredProducts = products
            .Where(x =>
            x.Name.ToLower().Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
            x.Category.Name.Contains(keyword, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        return filteredProducts;
    }
    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _appDbContext.Products
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        _appDbContext.Add(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Product> RemoveAsync(Product entity)
    {
        _appDbContext.Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        _appDbContext.Update(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }
}
