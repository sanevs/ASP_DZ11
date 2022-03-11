using System.Data.Entity;
using Glory.Domain;

namespace WebApp13_Backend;

public class CatalogRepository : EfRepository<ProductDTO>, ICatalogRepository
{
    private readonly ModelDbContext _context;

    public CatalogRepository(ModelDbContext context) : base(context)
    {
        _context = context;
    }
    
}