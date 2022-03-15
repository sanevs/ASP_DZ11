using Glory.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp13_Backend;

public class AccountRepository : IAccountRepository
{
    private readonly ModelDbContext _context;

    public AccountRepository(ModelDbContext context)
    {
        _context = context;
    }

    public async Task<IList<AccountDTO>> GetAll()=> 
        await _context.Accounts.ToListAsync();

    public async Task AddUser(AccountDTO account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}