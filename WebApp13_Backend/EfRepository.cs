using Microsoft.EntityFrameworkCore;

namespace WebApp13_Backend;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ModelDbContext _context;
    private DbSet<TEntity> _entities => _context.Set<TEntity>();
    public EfRepository(ModelDbContext context)
    {
        _context = context;
    }

    public virtual async Task<IList<TEntity>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public virtual async Task Add(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}