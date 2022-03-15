using Glory.Domain;

namespace WebApp13_Backend;

public class AccountService
{
    private readonly IAccountRepository _account;

    public AccountService(IAccountRepository account)
    {
        _account = account;
    }

    public async Task<IList<AccountDTO>> GetAll() => await _account.GetAll();
    public async Task AddUser(AccountDTO account) => await _account.AddUser(account);
}