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

    public async Task Add(string name, decimal price) =>
        await _repository.Add(name, price);

    public async Task Update() => await _repository.Update();

    public async Task AddToCart(int id) => await _repository.AddToCart(id);

    public async Task DeleteFromCart(int id) => await _repository.DeleteFromCart(id);
}