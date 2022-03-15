using Glory.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("cart"), ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    [HttpPost("addToCart")]
    public async Task AddToCart(ProductDTO product) => await _service.Add(product);
    
    [HttpPost("deleteFromCart")]
    public async Task DeleteProduct(ProductDTO product) => await _service.Delete(product);
}