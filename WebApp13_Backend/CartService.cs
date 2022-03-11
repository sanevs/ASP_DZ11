using Glory.Domain;

namespace WebApp13_Backend;

public class CartService
{
    private readonly ICartRepository _cart;

    public CartService(ICartRepository cart)
    {
        _cart = cart;
    }
    
    public async Task Add(ProductDTO product)  => await _cart.Add(product);

    public async Task Delete(ProductDTO product) => await _cart.Delete(product);
}