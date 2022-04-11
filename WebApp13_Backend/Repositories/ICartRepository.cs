using Glory.Domain;

namespace WebApp13_Backend;

public interface ICartRepository
{
    Task<IList<CartItem>?> GetCartItems(Guid accountId);
    Task Clear(Guid accountId);
    Task CreateCart(Guid accountId);
    Task Add(Guid accountId, ProductDTO product);
    Task Delete(Guid accountId, ProductDTO product);
}