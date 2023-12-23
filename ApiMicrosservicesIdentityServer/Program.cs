using ApiMicrosservicesIdentityServer.Extensions;
using ApiMicrosservicesIdentityServer.Identity.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseIdentityDI(builder.Configuration);
builder.Services.AddIdentityRulesDependecyInjection();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseSession();

await SeedUsersRoles(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task SeedUsersRoles(IApplicationBuilder builder)
{
    var scope = builder.ApplicationServices.CreateScope();
    var result = scope.ServiceProvider.GetService<ISeedRoleAndUser>();

    await result.SeedRoleAsync();
    await result.SeedUserAsync();
}
