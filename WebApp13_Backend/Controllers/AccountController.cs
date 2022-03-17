using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp13_Backend;

[Route("accounts"), ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service) 
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IList<AccountDTO>>> GetAll() => 
        Ok(await _service.GetAll());
    

    [HttpPost("addAccount")]
    public async Task<ActionResult> AddAccount([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        await _service.AddUser(hasher, account);
        return Ok();
    }

    [HttpPost("Authorize")]
    public async Task<ActionResult<AccountDTO>?> Authorize([FromServices] IPasswordHasher hasher, AccountRequestDTO account)
    {
        var accountResult = await _service.AuthorizeUser(hasher, account);
        if (accountResult == null)
            return Unauthorized();
        return Ok(accountResult);
    }
}