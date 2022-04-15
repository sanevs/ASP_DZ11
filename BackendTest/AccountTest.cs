using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Glory.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp13_Backend;
using Xunit;

namespace BackendTest;

public class AccountTest
{
    private readonly WebApplicationFactory<Program> _app;
    private readonly HttpClient _client;

    public AccountTest()
    {
        _app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(service =>
                {
                    using var provider = service.BuildServiceProvider();
                    using var scope = provider.CreateScope();
                    var context = scope.ServiceProvider.GetService<ModelDbContext>();
                    context.Database.EnsureCreated();
                });
            });
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task Add_Account_To_DB()
    {
        var account = new AccountRequestDTO("Test", "test@t.t", "qwerty", "admin");
        var message = await _client.PostAsJsonAsync("/accounts/addAccount", account);
        message.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, message.StatusCode);
    }

    [Fact]
    public async Task Add_Product_To_Cart()
    {
        var product = new ProductDTO(0, "TestProduct", 1002);
        var message = await _client.PostAsJsonAsync("/cart/addToCart", product);
        Assert.Equal(HttpStatusCode.OK, message.StatusCode);
    }
}