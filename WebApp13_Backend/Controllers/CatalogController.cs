using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("catalog"), ApiController]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _service;
    private readonly ILogger<CatalogService> _logger;

    public CatalogController(CatalogService service, ILogger<CatalogService> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("products")]
    public async Task<IList<ProductDTO>> GetAll()
    {
        var products = await _service.GetAll();
        foreach (var product in products)
            _logger.LogInformation(product.Name + " / " + product.Price);
        return products;
    }

    [HttpPost("addProduct")]
    public async Task AddProduct(ProductDTO product)
    {
        await _service.Add(product);
        _logger.LogInformation("Product added!");
    }
}