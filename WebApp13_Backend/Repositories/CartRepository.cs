using System.Data.Entity;
using Glory.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp13_Backend;

public class CartRepository : ICartRepository
{
    private readonly ModelDbContext _context;
    public CartRepository(ModelDbContext context) 
    {
        _context = context;
    }
    public async Task CreateCart(Guid accountId) => 
        await _context.Carts.AddAsync(new Cart(accountId));

    public async Task<IList<CartItem>?> GetCartItems(Guid accountId)
    {
        var cart = _context.Carts.SingleOrDefault(c => c.AccountId == accountId);
        return _context.CartItems.Where(i => i.CartId == cart.Id).ToList();
    }

    public async Task Add(Guid accountId, ProductDTO product)
    {
        var cart = _context.Carts.FirstOrDefault(c => c.AccountId == accountId);
        var existedItem = GetExistItem(cart.Id, product.Id);
        if (existedItem is not null)
        {
            existedItem.Quantity++;
        }
        else
        {
            _context.CartItems.Add(new CartItem(cart.Id, product.Id));
        }
    }

    public async Task Delete(Guid accountId, ProductDTO product)
    {
        var cart = _context.Carts.SingleOrDefault(c => c.AccountId == accountId);
        if (cart != null) _context.CartItems.Remove(GetExistItem(cart.Id, product.Id));
    }

    private CartItem? GetExistItem(Guid cartId, int productId) =>
        _context.CartItems.SingleOrDefault(
            p => 
                p.CartId == cartId && 
                p.ProductId == productId);
    
    // public override async Task Add(ProductDTO product)
    // {
    //     var foundProduct = await _context.Products.FindAsync(product.Id);
    //     if (foundProduct == null)
    //         return; 
    //     foundProduct.Quantity++;
    //     //await _context.SaveChangesAsync();
    // }
    //
    // public async Task Delete(ProductDTO product)
    // {
    //     var foundProduct = await _context.Products.FindAsync(product.Id);
    //     if (foundProduct == null || foundProduct.Quantity == 0)
    //         return; 
    //     foundProduct.Quantity = 0;
    //     //await _context.SaveChangesAsync();
    // }
}