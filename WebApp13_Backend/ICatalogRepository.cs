using Glory.Domain;

namespace WebApp13_Backend;

public interface ICatalogRepository
{
    Task<IList<ProductDTO>> GetAll();
    Task Add(ProductDTO product);
    
}