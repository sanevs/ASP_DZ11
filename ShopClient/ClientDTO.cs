using System.Net.Http.Json;
using System.Text.Json;
using Glory.Domain;
using Microsoft.Extensions.Logging;

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

    public Task AddProduct(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/catalog/addProduct", product);
    public Task AddAccount(AccountRequestDTO accountRequest) => 
        _client.PostAsJsonAsync($"{_uri}/accounts/addAccount", accountRequest);

    public async Task<AccountDTO?> Authorize(AccountRequestDTO accountRequest)
    {
        var message = await _client.PostAsJsonAsync($"{_uri}/accounts/Authorize", accountRequest);
        
        var accountJson = await message.Content.ReadAsStringAsync();
        
        return await Task.Run(() =>
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        
            return JsonSerializer.Deserialize(accountJson, typeof(AccountDTO), jsonSerializerOptions) as AccountDTO;
        });
        
        //var stream = await message.Content.ReadAsStreamAsync();
        //return await JsonSerializer.DeserializeAsync<AccountDTO>(stream);
        
    }

    public Task AddToCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/addToCart", product);
    
    public Task DeleteFromCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/deleteFromCart", product);
}