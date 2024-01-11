using ApiMicrosservicesProduct.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureModule(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddMapEndpointsService();
app.UseHttpsRedirection();


app.Run();

