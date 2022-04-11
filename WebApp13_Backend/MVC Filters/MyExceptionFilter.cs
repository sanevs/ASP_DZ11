using Glory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Validations.Rules;

namespace WebApp13_Backend.MVC_Filters;

public class MyExceptionFilter : Attribute, IExceptionFilter
{
    private readonly ILogger<MyExceptionFilter> _logger;

    public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        
        _logger.LogWarning("MVC Filter Exception Handler");
        var message = context.Exception switch
        {
            EmailNotFoundException => "Email not found", 
            WrongPasswordException => "Неправильный пароль",
            WrongCodeException => "Неправильный код подтверждения",
            HttpRequestException => "Ошибка http запроса",
            Exception => null
        };
        if (message != null)
        {
            context.Result = new ObjectResult(new {Message = message}); //результат метода в контроллере
            context.ExceptionHandled = true;
        }
    }
}