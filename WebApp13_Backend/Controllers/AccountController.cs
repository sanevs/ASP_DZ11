using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WebApp13_Backend;

[Route("accounts"), ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    private readonly ILogger<AccountController> _logger;
    private static string? _role;

    public AccountController(AccountService service, ILogger<AccountController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IList<AccountDTO>>> GetAll()
    {
        var claim = User.Claims.FirstOrDefault(it => it?.Type == "userRole");
        if (claim == null || claim.Value != "admin")
            return Ok(new List<AccountDTO>());
        var accounts = await _service.GetAll();
        // foreach (var account in accounts)
        //     _logger.LogInformation(account.Name + " / " + account.Email);
        return Ok(accounts);
    }
    
    [HttpPost("addAccount")]
    public async Task<ActionResult> AddAccount([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        await _service.AddUser(hasher, account);
        _logger.LogInformation("User added");
        return Ok();
    }

    [HttpPost("addRole")]
    public async Task<ActionResult> AddRole([FromBody] string? role)
    {
        _role = role;
        return Ok();
    }
    [HttpPost("Authorize")]
    public async Task<ActionResult<string>> Authorize([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        var accountResult = await _service.AuthorizeUser(hasher, account, _role);
        if (accountResult.account == null || accountResult.token == null)
        {
            _logger.LogWarning("Unauthorized access, wrong login or password");   
            return Unauthorized();
        }
        _logger.LogInformation($"User {accountResult.account.Name}({accountResult.account.Email}) entered!");
        return Ok(accountResult.account.Name + "/" + accountResult.token);
    }

    [HttpGet("getAccount")]
    public async Task<AccountDTO?> GetCurrentAccount()
    {
        var claim = User.Claims.FirstOrDefault(it => it?.Type == "account");
        if (claim == null)
        {
            return await Task.FromResult(new AccountDTO());
        }
        return await Task.FromResult(JsonSerializer.Deserialize<AccountDTO>(claim.Value)!);
    }
    
}