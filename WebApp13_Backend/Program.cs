using Microsoft.EntityFrameworkCore;
using WebApp13_Backend;

var builder = WebApplication.CreateBuilder(args);

string dbPath = "kw13.db";
builder.Services.AddDbContext<ModelDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapGet("/products", async (ModelDbContext context) => 
    await context.Products.ToListAsync());

app.MapGet("/addProduct/{id}/{name}/{price}", 
    async (ModelDbContext context, int id, string name, decimal price) =>
{
    await context.Products.AddAsync(new ProductDTO(id, name, price));
    
    await context.SaveChangesAsync();
});

app.MapGet("/p2", () => "Hi p2");
app.Run();