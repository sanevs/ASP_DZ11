using Glory.Domain;

namespace WebApp13_Backend;

public interface ICartRepository
{
    Task Add(ProductDTO product);
    Task Delete(ProductDTO product);
}