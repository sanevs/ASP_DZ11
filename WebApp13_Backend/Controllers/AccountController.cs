using System.Security.Claims;
using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp13_Backend.MVC_Filters.DI_Filters;

namespace WebApp13_Backend;

[Route("accounts"), ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    private readonly ILogger<AccountController> _logger;
    private readonly IPasswordHasher _hasher;

    public AccountController(AccountService service, ILogger<AccountController> logger, IPasswordHasher hasher)
    {
        _service = service;
        _logger = logger;
        _hasher = hasher;
    }

    [Authorize(Roles = "admin"), HttpGet("all")]
    public async Task<ActionResult<IList<AccountDTO>>> GetAll() => Ok(await _service.GetAll());

    [Authorize, HttpGet("getAccount"), TypeFilter(typeof(CheckBanFilter))]
    public async Task<AccountDTO?> GetCurrentAccount()
    {
        var claim = User.Claims.FirstOrDefault(it => it?.Type == ClaimTypes.NameIdentifier);
        return await _service.FindAccountById(Guid.Parse(claim.Value));
    }

    [HttpPost("addAccount")]
    public async Task<ActionResult<string?>> AddAccount(AccountRequestDTO account)
    {
        await _service.AddUser(_hasher, account);
        _logger.LogInformation("User added");
        return Ok($"User {account.Name} added");
    }

    [HttpPost("AuthorizeByPassword")]
    public async Task<ActionResult<string?>> AuthorizeByPassword(AccountRequestDTO account)
    {
        var idCode = await _service.AuthorizeByPassword(_hasher, account);
        _logger.LogWarning(idCode.Item2.ToString());
        return Ok(idCode.Item1 + "/" + idCode.Item2);
    }
    [HttpPost("AuthorizeByCode")]
    public async Task<ActionResult<string?>> AuthorizeByCode(TwoFA code)
    {
       var accountResult = await _service.AuthorizeByCode(code.Id, code.Code);
       _logger.LogInformation($"User {accountResult.account?.Name}({accountResult.account?.Email}) entered!");
        return Ok(accountResult.token);
    }
}