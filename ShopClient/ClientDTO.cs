using System.Net.Http.Headers;
using System.Net.Http.Json;
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
    public Task<AccountDTO?> GetAccount() =>
        _client.GetFromJsonAsync<AccountDTO?>($"{_uri}/accounts/getAccount");

    public Task AddProduct(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/catalog/addProduct", product);
    public async Task AddAccount(AccountRequestDTO accountRequest)
    {
        await _client.PostAsJsonAsync($"{_uri}/accounts/addAccount", accountRequest);
    }

    public async Task<AccountDTO?> Authorize(AccountRequestDTO accountRequest, string? role)
    {
        await _client.PostAsJsonAsync<string?>($"{_uri}/accounts/addRole", role);
        var message = await _client.PostAsJsonAsync($"{_uri}/accounts/Authorize", accountRequest);
        
        var result = await message.Content.ReadAsStringAsync();
        var name_token = result.Split('/');
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", name_token[1]);

        return await Task.Run(() => new AccountDTO(-1, name: name_token[0]));
        // return await Task.Run(() =>
        // {
        //     var jsonSerializerOptions = new JsonSerializerOptions
        //     {
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     };
        //
        //     return JsonSerializer.Deserialize(accountJson, typeof(AccountDTO), jsonSerializerOptions) as AccountDTO;
        // });
    }

    public Task AddToCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/addToCart", product);
    
    public Task DeleteFromCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/deleteFromCart", product);

}