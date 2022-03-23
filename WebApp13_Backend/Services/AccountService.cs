using Glory.Domain;
using Microsoft.AspNet.Identity;

namespace WebApp13_Backend;

public class AccountService
{
    private readonly IAccountRepository _account;

    public AccountService(IAccountRepository account)
    {
        _account = account;
    }

    public async Task<IList<AccountDTO>> GetAll() => await _account.GetAll();
    public async Task AddUser(IPasswordHasher hasher, AccountRequestDTO account) => 
        await _account.AddUser(hasher, account);

    public async Task<AccountDTO?> AuthorizeUser(IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        var accounts = await GetAll();
        var account = accounts?.First(
            a => a.Email == accountRequest.Email);
        if (account != null && 
            hasher.VerifyHashedPassword(account.HashedPassword, accountRequest.Password) == 
            PasswordVerificationResult.Success)
            return account;
        return null;
    }
}