using Glory.Domain;

namespace WebApp13_Backend;

public interface ICartRepository : IRepository<ProductDTO>
{
    Task Delete(ProductDTO product);
}