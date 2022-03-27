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
    public Task AddAccount(AccountRequestDTO accountRequest) => 
        _client.PostAsJsonAsync($"{_uri}/accounts/addAccount", accountRequest);

    public async Task<string?> Authorize(AccountRequestDTO accountRequest)
    {
        var message = await _client.PostAsJsonAsync($"{_uri}/accounts/Authorize", accountRequest);
        return await message.Content.ReadAsStringAsync();
    }

    public Task AddToCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/addToCart", product);
    
    public Task DeleteFromCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/deleteFromCart", product);

    public void SetToken(string token) =>
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
}