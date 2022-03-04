using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using Microsoft.EntityFrameworkCore;
using WebApp13_Backend;

var builder = WebApplication.CreateBuilder(args);

string dbPath = "kw13.db";
builder.Services.AddDbContext<ModelDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.MapGet("/products", async (ModelDbContext context) => 
    await context.Products.ToListAsync());

app.MapGet("/addProduct/{name}/{price}", 
    async (ModelDbContext context, string name, decimal price) =>
    {
        int maxId = context.Products
            .Select(p => p.Id).Max() + 1;
    await context.Products.AddAsync(new ProductDTO(maxId, name, price));
    
    await context.SaveChangesAsync();
});

app.MapGet("/addToCart/{id}", 
    async (ModelDbContext context, int id) =>
    {
        var foundProduct = await context.Products.FindAsync(id);
        if (foundProduct == null)
            return; 
        foundProduct.Quantity++;
        await context.SaveChangesAsync();
    });

app.MapGet("/deleteFromCart/{id}", async (ModelDbContext context, int id) =>
{
        var foundProduct = await context.Products.FindAsync(id);
        if (foundProduct == null || foundProduct.Quantity == 0)
            return; 
        foundProduct.Quantity = 0;
        await context.SaveChangesAsync();
});

app.Run();