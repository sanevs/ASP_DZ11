namespace WebApp13_Backend;

public class BrowserMiddleware
{
    private readonly RequestDelegate _next;

    public BrowserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(true)
        // if (context.Request.Headers["sec-ch-ua"]
        //     .Any( s => s.Contains("Edge") || s.Contains("Chrome")))
        {
            await _next(context);
        }
        else
        {
            await context.Response.WriteAsync("Error browser");
        }
    }
}