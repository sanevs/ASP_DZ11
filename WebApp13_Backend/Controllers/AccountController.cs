using System.Reflection.Metadata.Ecma335;
using Glory.Domain;
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
    public async Task<ActionResult> AddProduct(AccountDTO account)
    {
        await _service.AddUser(account);
        return Ok(); 
    }
}