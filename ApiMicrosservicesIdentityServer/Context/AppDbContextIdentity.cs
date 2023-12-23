using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesIdentityServer.Context;
public class AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : IdentityDbContext<ApplicationUser>(options)
{
}
