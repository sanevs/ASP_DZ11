using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging.Abstractions;

namespace WebApp13_Backend.MVC_Filters;

public class MyActionFilter : Attribute, IActionFilter
{
    private TimeSpan _interval;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _interval = new TimeSpan(DateTime.Now.Ticks);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var timeSpan = _interval.Subtract(new TimeSpan(DateTime.Now.Ticks));
        Console.WriteLine(
            $"Action executing time is {Math.Abs(timeSpan.Seconds)} seconds and {Math.Abs(timeSpan.Milliseconds)} ms");
    }
}