using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApp13_Backend.UoW;

namespace WebApp13_Backend;

public class AccountService
{
    private readonly IUnitOfWork _uow;
    private readonly JwtConfig _jwtConfig;

    public AccountService(IUnitOfWork uow, JwtConfig jwtConfig)
    {
        _uow = uow;
        _jwtConfig = jwtConfig;
    }

    public async Task<IList<AccountDTO>> GetAll() => await _uow.AccountRepository.GetAll();
    public async Task AddUser(IPasswordHasher hasher, AccountRequestDTO account)
    {
        var addedUserId = await _uow.AccountRepository.AddUser(hasher, account);
        await _uow.CartRepository.CreateCart(addedUserId);
        await _uow.SaveChangesAsync();
    }

    public async Task<(AccountDTO? account, string token)> AuthorizeUser(
        IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        var accounts = await GetAll();
        var account = accounts?.First(a => a.Email == accountRequest.Email);
        if (account != null &&
            hasher.VerifyHashedPassword(account.HashedPassword, accountRequest.Password) ==
            PasswordVerificationResult.Success)
        {
            return (account, GenerateToken(account.Id.ToString(), account.Role));
        }
        return (new AccountDTO(Guid.Empty), string.Empty);
    }

    public async Task<AccountDTO?> FindAccountById(Guid id) => 
        (await GetAll()).FirstOrDefault(a => a.Id == id);

    private string GenerateToken(string id, string role)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        { 
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}