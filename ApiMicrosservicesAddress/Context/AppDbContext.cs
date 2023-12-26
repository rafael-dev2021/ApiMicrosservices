using ApiMicrosservicesAddress.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesAddress.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<AddressDto> AddressDtos { get; set; }

      
    }
}
