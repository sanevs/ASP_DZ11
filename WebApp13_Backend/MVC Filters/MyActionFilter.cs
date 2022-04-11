using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging.Abstractions;

namespace WebApp13_Backend.MVC_Filters;

public class MyActionFilter : Attribute, IActionFilter
{
    private TimeSpan _start;
    private readonly Stopwatch _stopwatch = new();

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _start = new TimeSpan(DateTime.Now.Ticks); 
        _stopwatch.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var timeSpan = _start.Subtract(new TimeSpan(DateTime.Now.Ticks));
        _stopwatch.Stop();
        var interval = _stopwatch.Elapsed;
        Console.WriteLine(
            $"Action executing time is {Math.Abs(timeSpan.Seconds)} seconds and {Math.Abs(timeSpan.Milliseconds)} ms");
        Console.WriteLine(
            $"(StopWatch)Action executing time is {Math.Abs(interval.Seconds)} seconds and {Math.Abs(interval.Milliseconds)} ms");
    }
}