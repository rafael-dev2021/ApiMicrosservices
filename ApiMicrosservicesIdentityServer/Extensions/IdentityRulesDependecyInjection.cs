using ApiMicrosservicesIdentityServer.Context;
using ApiMicrosservicesIdentityServer.Extensions.Filters;
using ApiMicrosservicesIdentityServer.Identity;
using ApiMicrosservicesIdentityServer.Identity.Configuration;
using ApiMicrosservicesIdentityServer.Identity.Interfaces;
using ApiMicrosservicesIdentityServer.Identity.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;

namespace ApiMicrosservicesIdentityServer.Extensions;

public static class IdentityRulesDependecyInjection
{
    public static IServiceCollection AddIdentityRulesDependecyInjection(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContextIdentity>()
                .AddDefaultTokenProviders();

        //configurações dos serviços do IdentityServer
        services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            options.EmitStaticAudienceClaim = true;
        }).AddInMemoryIdentityResources(
                               IdentityConfiguration.IdentityResources)
                               .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                               .AddInMemoryClients(IdentityConfiguration.Clients)
                               .AddAspNetIdentity<ApplicationUser>()
                               .AddDeveloperSigningCredential();

        services.AddScoped<ISeedRoleAndUser, SeedRoleAndUser>();
        services.AddScoped<IProfileService, ProfileAppService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddHttpContextAccessor();
        services.AddSession();
        services.AddMemoryCache();


        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(15);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;
        });

        services.Configure<PasswordOptions>(options =>
        {
            options.RequireDigit = true;
            options.RequireLowercase = true;
            options.RequireUppercase = true;
            options.RequireNonAlphanumeric = true;
            options.RequiredLength = 8;
            options.RequiredUniqueChars = 6;
        });

        services.AddAuthorizationBuilder()
           .AddPolicy("Admin", policy =>
           {
               policy.RequireRole("Admin");
           });

        services.AddHttpsRedirection(options =>
        {
            options.HttpsPort = null;
        });


        services.AddMvc(options =>
        {
            options.Filters.Add(new SecurityHeadersAttribute());
        });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromMinutes(15);
        });

        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.IsEssential = true;

            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

        return services;
    }
}
