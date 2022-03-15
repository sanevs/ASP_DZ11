using Glory.Domain;

namespace WebApp13_Backend;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
}