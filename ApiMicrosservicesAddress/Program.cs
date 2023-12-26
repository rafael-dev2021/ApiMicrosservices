using ApiMicrosservicesAddress.Endpoints;
using ApiMicrosservicesAddress.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJsonSettings();
builder.Services.AddDatabaseDependecyInjection(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapAddressServiceEndpoints();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");


app.Run();

