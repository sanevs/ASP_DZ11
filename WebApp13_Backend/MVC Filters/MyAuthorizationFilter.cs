using System.Reflection.Metadata.Ecma335;
using Glory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp13_Backend.MVC_Filters;

public class MyAuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly ILogger<MyAuthorizationFilter> _logger;

    public MyAuthorizationFilter(ILogger<MyAuthorizationFilter> logger)
    {
        _logger = logger;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _logger.LogInformation("MVC Filter Authorization Handler");
        var apiHeader = context.HttpContext.Request.Headers["apikey"].ToString();
        if (apiHeader == string.Empty)
            context.Result = new UnauthorizedResult();
    }
}