using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp13_Backend;
using Xunit;

namespace BackendTest;

public class MiddlewareTest
{
    [Fact]
    public void Correct_Browser()
    {
        var passed = false;
        BrowserMiddleware browserMiddleware = new BrowserMiddleware( _ =>
        {
            passed = true;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.Headers.Add("sec-ch-ua", "Edge");

        browserMiddleware.InvokeAsync(context);
        Assert.True(passed);
    }
}