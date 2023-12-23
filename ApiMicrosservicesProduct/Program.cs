using ApiMicrosservicesProduct.EndPoints;
using ApiMicrosservicesProduct.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBaseDependecyInjection(builder.Configuration);

builder.Services.AddAuthenticationDI(builder.Configuration);

builder.Services.AddRepositoryDependecyInjection();

builder.Services.AddServiceDependecyInjection();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorizationBuilder()
           .AddPolicy("Admin", policy =>
           {
               policy.RequireRole("Admin");
           });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapCategoryServiceEndpoints();
app.MapProductServiceEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
