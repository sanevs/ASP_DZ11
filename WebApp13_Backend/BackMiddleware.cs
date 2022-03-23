
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
        LogHeaders(context.Request.Headers);
        LogHeaders(context.Response.Headers);

        context.Request.EnableBuffering();
        if (context.Request.Body.CanRead)
        {
            using var reader = new StreamReader(context.Request.Body, 
                Encoding.UTF8, false, leaveOpen: true);
            string message = await reader.ReadToEndAsync();
            _logger.LogInformation(message);
            context.Request.Body.Position = 0;
        }
        
        //var stream = context.Request.Body;
        //_logger.LogInformation(stream);
        //_logger.LogInformation(context.Response.Body.ToString());
        await _next(context);
    }

    private void LogHeaders(IHeaderDictionary headers)
    {
        var headersRequestList = headers
            .Select(h => 
                string.Concat(h.Key, " / ", h.Value));
        foreach (var header in headersRequestList)
        {
            _logger.LogInformation(header);
        }
    }
}