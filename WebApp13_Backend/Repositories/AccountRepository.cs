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

    public async Task<Guid> AddUser(IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        Guid guid = Guid.NewGuid();
        AccountDTO account = new AccountDTO(
            guid, 
            isBanned: false,
            accountRequest.Name, 
            accountRequest.Email, 
            hasher.HashPassword(accountRequest.Password),
            accountRequest.Role);
        await _context.Accounts.AddAsync(account);
        return guid;
        //await _context.SaveChangesAsync();
    }

    public async Task AddCode(Guid id, Guid accountId, int code) => 
        await _context.Codes.AddAsync(new TwoFA(id, accountId, code));

    public async Task<Guid?> GetUserId(Guid codeId, int code) => 
        _context.Codes.FirstOrDefault(c => c.Id == codeId && c.Code == code)?.AccountId ?? null;
}