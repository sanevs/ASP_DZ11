using Glory.Domain;

namespace WebApp13_Backend;

public interface ICatalogRepository
{
    Task<IList<ProductDTO>> GetAll();
    Task Add(string name, decimal price);
    Task Update();
    Task AddToCart(int id);
    Task DeleteFromCart(int id);
    
}