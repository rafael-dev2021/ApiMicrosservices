using ApiMicrosservicesDiscount.Context;
using ApiMicrosservicesDiscount.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMicrosservicesDiscount", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
               },
               Scheme = "oauth2",
               Name = "Bearer",
               In= ParameterLocation.Header
            },
            new List<string> ()
         }
    });
});

builder.Services.AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options =>
       {
           options.Authority =
            builder.Configuration["ApiMicrosservicesIdentityServer:ApplicationUrl"];

           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false
           };
       });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "apiMicrosservices");
    });



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

//app.MapGet("/discount/{couponCode}", async (HttpContext context, string couponCode) =>
//{
//    var repository = context.RequestServices.GetRequiredService<ICouponRepository>();
//    var coupon = await repository.GetCouponByCode(couponCode);

//    if (coupon is null)
//    {
//        context.Response.StatusCode = StatusCodes.Status404NotFound;
//        await context.Response.WriteAsync($"Coupon Code: {couponCode} not found");
//        return;
//    }

//    await context.Response.WriteAsJsonAsync(coupon);
//});

app.MapGet("/api/discount/{couponCode}", async (HttpContext context, string couponCode, [FromServices] ICouponRepository repository) =>
{
    var coupon = await repository.GetCouponByCode(couponCode);

    if (coupon is null)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync($"Coupon Code: {couponCode} not found");
        return;
    }

    await context.Response.WriteAsJsonAsync(coupon);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
