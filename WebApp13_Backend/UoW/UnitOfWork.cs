namespace WebApp13_Backend.UoW;

public class UnitOfWork :IUnitOfWork, IAsyncDisposable
{
    public IAccountRepository AccountRepository { get; }
    public ICartRepository CartRepository { get; }
    public ICatalogRepository CatalogRepository { get; }
    private readonly ModelDbContext _dbContext;

    public UnitOfWork(ModelDbContext dbContext, IAccountRepository accountRepository, 
        ICartRepository cartRepository, ICatalogRepository catalogRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        CatalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => 
        _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}