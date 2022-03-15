using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp13_Backend;

var builder = WebApplication.CreateBuilder(args);

string dbPath = "kw13.db";
builder.Services.AddDbContext<ModelDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped(typeof(IRepository<ProductDTO>), typeof(EfRepository<ProductDTO>));

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<CatalogService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CartService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

//app.MapGet("/products", async (CatalogService service) => 
//    await service.GetAll());
//
// app.MapPost("/addProduct/{name}/{price}", 
//     async (/*Product product -> при использовании PostAsJsonAsync(product)*/
//         CatalogService service, string name, int price) =>
//     await service.Add(name, price));
//
// app.MapPost("/addToCart/{id}",
//     async (CatalogService service, int id) =>
//         await service.AddToCart(id));
//
// app.MapPost("/deleteFromCart/{id}", async (CatalogService service, int id) =>
//     await service.DeleteFromCart(id));

app.Run();