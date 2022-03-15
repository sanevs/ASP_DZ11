using Glory.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("catalog"), ApiController]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _service;

    public CatalogController(CatalogService service)
    {
        _service = service;
    }

    [HttpGet("products")]
    public async Task<IList<ProductDTO>> GetAll() => await _service.GetAll();

    [HttpPost("addProduct")]
    public async Task AddProduct(ProductDTO product) => await _service.Add(product);
}