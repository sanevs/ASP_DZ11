using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("accounts"), ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AccountService service, ILogger<AccountController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IList<AccountDTO>>> GetAll()
    {
        var accounts = await _service.GetAll();
        foreach (var account in accounts)
            _logger.LogInformation(account.Name + " / " + account.Email);
        return Ok(accounts);
    }


    [HttpPost("addAccount")]
    public async Task<ActionResult> AddAccount([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        await _service.AddUser(hasher, account);
        _logger.LogInformation("User added");
        return Ok();
    }

    [HttpPost("Authorize")]
    public async Task<ActionResult<AccountDTO>?> Authorize([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        var accountResult = await _service.AuthorizeUser(hasher, account);
        if (accountResult == null)
        {
            _logger.LogWarning("Unauthorized access, wrong login or password");   
            return Unauthorized();
        }
        _logger.LogInformation($"User {accountResult.Name}({accountResult.Email}) entered!");
        return Ok(accountResult);
    }
}