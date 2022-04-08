using Glory.Domain;
using Microsoft.AspNet.Identity;

namespace WebApp13_Backend;

public interface IAccountRepository
{
    Task<IList<AccountDTO>> GetAll();
    Task<Guid> AddUser(IPasswordHasher hasher, AccountRequestDTO accountRequest);
    Task AddCode(Guid id, Guid accountId, int code);
    Task<Guid?> GetUserId(Guid codeId, int code);
}