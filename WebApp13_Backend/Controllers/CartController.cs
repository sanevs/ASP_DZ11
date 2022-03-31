using System.Security.Claims;
using Glory.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("cart"), ApiController, Authorize]
public class CartController : ControllerBase
{
    private readonly CartService _service;
    private readonly ILogger<CartController> _logger;

    public CartController(CartService service, ILogger<CartController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("getCartProducts")]
    public async Task<IList<ProductDTO>?> GetCartProducts() => 
        await _service.GetCartProducts(GetUserId());

    [HttpPost("addToCart")]
    public async Task AddToCart(ProductDTO product)
    {
        await _service.Add(GetUserId(), product);
        _logger.LogInformation("Product is added to cart");
    }

    [HttpPost("deleteFromCart")]
    public async Task DeleteProduct(ProductDTO product)
    {
        await _service.Delete(GetUserId(), product);
        _logger.LogInformation("Product is deleted from cart");
    }

    private Guid GetUserId()
    {
        var claim = User.Claims.FirstOrDefault(it => it?.Type == ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(claim);
    }
}