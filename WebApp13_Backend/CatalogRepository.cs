using System.Net;
using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp13_Backend;

public class CatalogRepository : ICatalogRepository
{
    private readonly ModelDbContext _context;

    public CatalogRepository(ModelDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ProductDTO>> GetAll() => 
        await _context.Products.ToListAsync();

    public async Task Add(string name, decimal price)
    {
        int maxId = _context.Products
            .Select(p => p.Id).Max() + 1;
        await _context.Products.AddAsync(new ProductDTO(maxId, name, price));
        await Update();
    }

    public async Task Update() => await _context.SaveChangesAsync();

    public async Task AddToCart(int id)
    {
        var foundProduct = await _context.Products.FindAsync(id);
        if (foundProduct == null)
            return; 
        foundProduct.Quantity++;
        await Update();
    }

    public async Task DeleteFromCart(int id)
    {
        var foundProduct = await _context.Products.FindAsync(id);
        if (foundProduct == null || foundProduct.Quantity == 0)
            return; 
        foundProduct.Quantity = 0;
        await Update();
    }
}