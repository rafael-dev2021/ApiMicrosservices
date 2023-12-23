using ApiMicrosservicesWeb.Extends;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthenticationDI(builder.Configuration);
builder.Services.AddHttpClientDI(builder.Configuration);

builder.Services.AddAuthorizationBuilder()
.AddPolicy("Admin", policy =>
{
    policy.RequireRole("Admin");
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
