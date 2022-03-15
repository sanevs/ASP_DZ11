using Glory.Domain;

namespace WebApp13_Backend;

public class CartRepository : EfRepository<ProductDTO>, ICartRepository
{
    private readonly ModelDbContext _context;
    public CartRepository(ModelDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task Add(ProductDTO product)
    {
        var foundProduct = await _context.Products.FindAsync(product.Id);
        if (foundProduct == null)
            return; 
        foundProduct.Quantity++;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(ProductDTO product)
    {
        var foundProduct = await _context.Products.FindAsync(product.Id);
        if (foundProduct == null || foundProduct.Quantity == 0)
            return; 
        foundProduct.Quantity = 0;
        await _context.SaveChangesAsync();
    }
}