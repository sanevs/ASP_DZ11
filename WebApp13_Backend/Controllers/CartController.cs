using Glory.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("cart"), ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _service;
    private readonly ILogger<CartController> _logger;

    public CartController(CartService service, ILogger<CartController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("addToCart")]
    public async Task AddToCart(ProductDTO product)
    {
        await _service.Add(product);
        _logger.LogInformation("Product is added to cart");
    }

    [HttpPost("deleteFromCart")]
    public async Task DeleteProduct(ProductDTO product)
    {
        await _service.Delete(product);
        _logger.LogInformation("Product is deleted from cart");
    }
}