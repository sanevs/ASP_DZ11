
using System.Text;
using System.Text.Json;

namespace WebApp13_Backend;

public class BackMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<BackMiddleware> _logger;

    public BackMiddleware(RequestDelegate next, ILogger<BackMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request {r}", context.Request.Headers);
        _logger.LogInformation("Response {r}", context.Response.Headers);
    
        context.Request.EnableBuffering();
        LogBody(context.Request.Body);
        LogBody(context.Response.Body);
        
        await _next(context);
    }
    
    private async void LogBody(Stream body)
    {
        if (!body.CanRead)
            return;
        using var reader = new StreamReader(body, 
            Encoding.UTF8, false, leaveOpen: true);
        string message = await reader.ReadToEndAsync();
        _logger.LogInformation(message);
        body.Position = 0;
    }
}