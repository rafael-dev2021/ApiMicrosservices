using ApiMicrosservicesProduct.Models;
using ApiMicrosservicesProduct.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesProduct.Context.Repositories
{
    public class CategoryRepository(AppDbContext appDbContext) : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<IEnumerable<Category>> GetItemsAsync()
        {
            return await _appDbContext.Categories
                .AsNoTracking()
                .Include(x => x.Products)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int? id)
        {
            return await _appDbContext.Categories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            _appDbContext.Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> RemoveAsync(Category entity)
        {
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

    }
}