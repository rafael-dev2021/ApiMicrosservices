using ApiMicrosservicesProduct.EndPoints;
using ApiMicrosservicesProduct.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBaseDependecyInjection(builder.Configuration);
builder.Services.AddRepositoryDependecyInjection();
builder.Services.AddServiceDependecyInjection();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoryServiceEndpoints();
app.MapProductServiceEndpoints();
app.UseHttpsRedirection();

app.Run();
