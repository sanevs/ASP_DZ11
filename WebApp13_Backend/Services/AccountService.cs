using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;

namespace WebApp13_Backend;

public class AccountService
{
    private readonly IAccountRepository _account;
    private readonly JwtConfig _jwtConfig;

    public AccountService(IAccountRepository account, JwtConfig jwtConfig)
    {
        _account = account;
        _jwtConfig = jwtConfig;
    }

    public async Task<IList<AccountDTO>> GetAll() => await _account.GetAll();
    public async Task AddUser(IPasswordHasher hasher, AccountRequestDTO account) => 
        await _account.AddUser(hasher, account);

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
        return (new AccountDTO(), string.Empty);
    }

    public async Task<AccountDTO?> FindAccountById(int id) => 
        (await GetAll()).FirstOrDefault(a => a.Id == id);

    public string GenerateToken(string id, string role)
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