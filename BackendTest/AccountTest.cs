using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Glory.Domain;
using ShopClient;
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
    private readonly ClientDTO _client;
    public AccountTest()
    {
        var app = new WebApplicationFactory<Program>()
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
        _client = new ClientDTO("", app.CreateClient());
    }

    [Fact]
    public async Task Add_Account_To_DB()
    {
        var account = new AccountRequestDTO("Test", "test@t.t", "qwerty", "admin");
        var result = await _client.AddAccount(account);
        Assert.Equal($"User {account.Name} added", result);
    }

    [Fact]
    public async Task Log_In_By_2_Factor_Authentication()
    {
        var account = new AccountRequestDTO("Test", "test@t.t", "qwerty", "admin");
        var code = await _client.AuthorizeByPassword(account);
        Assert.DoesNotContain("message", code);

        var tfa = code.Split('/');
        var token = await _client.AuthorizeByCode(
            new TwoFA(Guid.Parse(tfa[0]), Guid.Empty, int.Parse(tfa[1])));

        _client.SetToken(token);
        var accountResult = await _client.GetAccount("");
        Assert.Equal(account.Email, accountResult.Email);
    }
}