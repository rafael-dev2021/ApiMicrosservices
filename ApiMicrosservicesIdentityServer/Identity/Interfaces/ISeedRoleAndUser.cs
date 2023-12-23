namespace ApiMicrosservicesIdentityServer.Identity.Interfaces
{
    public interface ISeedRoleAndUser
    {
        Task SeedRoleAsync();
        Task SeedUserAsync();
    }
}
