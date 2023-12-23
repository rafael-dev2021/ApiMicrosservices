using Microsoft.AspNetCore.Identity;

namespace ApiMicrosservicesIdentityServer.Context;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
