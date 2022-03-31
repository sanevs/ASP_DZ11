namespace WebApp13_Backend.UoW;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }
    ICatalogRepository CatalogRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}