using Glory.Domain;
using Microsoft.AspNet.Identity;

namespace WebApp13_Backend;

public interface IAccountRepository
{
    Task<IList<AccountDTO>> GetAll();
    Task AddUser(IPasswordHasher hasher, AccountRequestDTO accountRequest);
    Task<AccountDTO?> AuthorizeUser(IPasswordHasher hasher, AccountRequestDTO accountRequest);
}