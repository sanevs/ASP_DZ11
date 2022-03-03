using System.Data;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApp13_Backend;

public class ModelDbContext : DbContext
{
    public DbSet<ProductDTO> Products => Set<ProductDTO>();

    public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
    {
        
    }
}