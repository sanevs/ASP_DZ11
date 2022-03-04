using Glory.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp13_Backend;

public class ModelDbContext : DbContext
{
    public DbSet<ProductDTO> Products => Set<ProductDTO>();

    public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
    {
        
    }
}