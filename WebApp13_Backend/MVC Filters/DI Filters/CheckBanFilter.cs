using System.Security.Claims;
using Glory.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp13_Backend.MVC_Filters.DI_Filters;

public class CheckBanFilter : IAsyncAuthorizationFilter
{
    private readonly AccountService _service;
    
    public CheckBanFilter(AccountService service)
    {
        _service = service;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (await _service.CheckBannedUser(
                Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(
                    it => it?.Type == ClaimTypes.NameIdentifier)
                    ?.Value)))
        {
            context.Result = new ObjectResult (new AccountDTO(Guid.Empty, true, "Пользователь забанен"));
        }
            
    }
}