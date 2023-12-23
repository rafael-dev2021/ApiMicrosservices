using ApiMicrosservicesIdentityServer.Context;
using ApiMicrosservicesIdentityServer.Identity.Configuration;
using ApiMicrosservicesIdentityServer.Identity.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ApiMicrosservicesIdentityServer.Identity;

public class SeedRoleAndUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedRoleAndUser
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;


    public async Task SeedRoleAsync()
    {
        if (!await _roleManager.RoleExistsAsync(IdentityConfiguration.Admin))
        {
            IdentityRole role = new()
            {
                Name = IdentityConfiguration.Admin,
                NormalizedName = IdentityConfiguration.Admin.ToUpper()
            };

            _ = await _roleManager.CreateAsync(role);
        }

        if (!await _roleManager.RoleExistsAsync(IdentityConfiguration.Client))
        {
            IdentityRole role = new()
            {
                Name = IdentityConfiguration.Client,
                NormalizedName = IdentityConfiguration.Client.ToUpper()
            };
            _ = await _roleManager.CreateAsync(role);
        }
    }

    public async Task SeedUserAsync()
    {
        if (await _userManager.FindByEmailAsync("admin@localhost.com") == null)
        {
            var applicationUser = new ApplicationUser
            {
                Email = "admin@localhost.com",
                UserName = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                FirstName = "Rafael",
                LastName = "Silva",
                BirthDate = new DateTime(1998, 06, 14),
                CPF = "123.456.789.10",
                PhoneNumber = "+55 (79) 94002-8922",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, "@Visual23k+");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, IdentityConfiguration.Admin);

                _ = await _userManager.AddClaimsAsync(applicationUser, new Claim[]
                {
          new(JwtClaimTypes.Name, $"{applicationUser.FirstName} {applicationUser.LastName}"),
          new(JwtClaimTypes.GivenName, applicationUser.FirstName),
          new(JwtClaimTypes.FamilyName, applicationUser.LastName),
          new(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                });
            }
        }

        if (await _userManager.FindByEmailAsync("user@localhost.com") == null)
        {
            var applicationUser = new ApplicationUser
            {
                Email = "user@localhost.com",
                UserName = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                NormalizedUserName = "USER@LOCALHOST.COM",
                FirstName = "Rafael",
                LastName = "Silva",
                BirthDate = new DateTime(1998, 06, 14),
                CPF = "123.456.789.10",
                PhoneNumber = "+55 (79) 94002-8922",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, "@Visual23k+");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, IdentityConfiguration.Client);
                _ = await _userManager.AddClaimsAsync(applicationUser, new Claim[]
                {
          new(JwtClaimTypes.Name, $"{applicationUser.FirstName} {applicationUser.LastName}"),
          new(JwtClaimTypes.GivenName, applicationUser.FirstName),
          new(JwtClaimTypes.FamilyName, applicationUser.LastName),
          new(JwtClaimTypes.Role, IdentityConfiguration.Client)
                });
            }
        }
    }
}