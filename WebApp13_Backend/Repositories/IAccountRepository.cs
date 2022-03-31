using Glory.Domain;
using Microsoft.AspNet.Identity;

namespace WebApp13_Backend;

public interface IAccountRepository
{
    Task<IList<AccountDTO>> GetAll();
    Task<Guid> AddUser(IPasswordHasher hasher, AccountRequestDTO accountRequest);
}