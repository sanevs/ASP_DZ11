using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
using Microsoft.AspNet.Identity;
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

    public async Task AddUser(IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        AccountDTO account = new AccountDTO(
            0, 
            accountRequest.Name, 
            accountRequest.Email, 
            hasher.HashPassword(accountRequest.Password));
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task<AccountDTO?> AuthorizeUser(IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(
                a => a.Email == accountRequest.Email);
        if (account != null && 
            hasher.VerifyHashedPassword(account.HashedPassword, accountRequest.Password) == 
            PasswordVerificationResult.Success)
            return account;
        return null;
    }
}