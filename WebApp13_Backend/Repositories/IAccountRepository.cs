using Glory.Domain;

namespace WebApp13_Backend;

public interface IAccountRepository
{
    Task<IList<AccountDTO>> GetAll();
    Task AddUser(AccountDTO account);
}