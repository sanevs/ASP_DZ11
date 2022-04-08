using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using Glory.Domain;

namespace ShopClient;

public class ClientDTO
{
    private readonly string _uri;
    private readonly HttpClient _client;
    
    public ClientDTO(string uri, HttpClient client)
    {
        _uri = uri;
        _client = client;
    }

    public async Task<IList<ProductDTO>?> GetProducts() =>
        await _client.GetFromJsonAsync<IList<ProductDTO>>(
            $"{_uri}/catalog/products");

    public Task<IList<AccountDTO>?> GetAccounts() =>
        _client.GetFromJsonAsync<IList<AccountDTO>>($"{_uri}/accounts/all");
    public async Task<AccountDTO?> GetAccount(string key)
    {
        var user = await _client.GetFromJsonAsync<AccountDTO?>($"{_uri}/accounts/getAccount");
        //await AddApiKey(key);
        return user;
    }

    public Task AddProduct(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/catalog/addProduct", product);
    public Task AddAccount(AccountRequestDTO accountRequest) => 
        _client.PostAsJsonAsync($"{_uri}/accounts/addAccount", accountRequest);

    public async Task<string?> AuthorizeByPassword(AccountRequestDTO accountRequest)
    {
        var message = await _client.PostAsJsonAsync($"{_uri}/accounts/AuthorizeByPassword", accountRequest);
        return await message.Content.ReadAsStringAsync();
    }
    public async Task<string?> AuthorizeByCode(TwoFA code)
    {
        var message = await _client.PostAsJsonAsync($"{_uri}/accounts/AuthorizeByCode", code);
        return await message.Content.ReadAsStringAsync();
    }

    public async Task<IList<ProductDTO>?> GetCartProducts() => 
        await _client.GetFromJsonAsync<IList<ProductDTO>?>($"{_uri}/cart/getCartProducts");

    public Task AddToCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/addToCart", product);
    
    public Task DeleteFromCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/deleteFromCart", product);

    public void SetToken(string token) =>
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);

    public Task AddApiKey(string key)
    {
        if(!_client.DefaultRequestHeaders.TryGetValues("apikey", out _))
            _client.DefaultRequestHeaders.Add("apikey", key);
        return Task.CompletedTask;
    }
}