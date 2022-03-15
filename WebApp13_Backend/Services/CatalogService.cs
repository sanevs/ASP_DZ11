using Glory.Domain;

namespace WebApp13_Backend;

public class CatalogService
{
    private readonly ICatalogRepository _repository;

    public CatalogService(ICatalogRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductDTO>> GetAll() =>
        await _repository.GetAll();

    public async Task Add(ProductDTO product) =>
        await _repository.Add(product);

}