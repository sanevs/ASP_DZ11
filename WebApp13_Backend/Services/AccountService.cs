using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Glory.Domain;
using Glory.Domain.Exceptions;
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

    private Task<int> GenerateCode() => Task.FromResult(new Random().Next(100_000, 1_000_000));

    public async Task<(Guid, int?)> AuthorizeByPassword(IPasswordHasher hasher, AccountRequestDTO accountRequest)
    {
        var accounts = await GetAll();
        var account = accounts.FirstOrDefault(a => a.Email == accountRequest.Email);
        if (account == null)
            throw new EmailNotFoundException();

        if (hasher.VerifyHashedPassword(
                account.HashedPassword, accountRequest.Password) != PasswordVerificationResult.Success) 
            throw new WrongPasswordException();
        
        var guid = Guid.NewGuid();
        var code = await GenerateCode();
        await _uow.AccountRepository.AddCode(guid, account.Id, code);
        await _uow.SaveChangesAsync();
        return (guid, code);
    }
    public async Task<(AccountDTO? account, string token)> AuthorizeByCode(Guid codeId, int code)
    {
        var userId = await _uow.AccountRepository.GetUserId(codeId, code);
        var account = await FindAccountById(userId);
        if (account is null)
            throw new WrongCodeException();
        return (account, GenerateToken(account.Id.ToString(), account.Role));
    }

    public async Task<AccountDTO?> FindAccountById(Guid? id) => 
        (await GetAll()).FirstOrDefault(a => a.Id == id);

    public async Task<bool> CheckBannedUser(Guid? id)
    {
        var account = await FindAccountById(id);
        if(account is not null)
            return account.IsBanned;
        return false;
    }
    
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