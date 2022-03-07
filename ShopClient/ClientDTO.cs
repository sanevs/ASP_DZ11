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

    public Task<IList<ProductDTO>?> GetProducts() =>
        _client.GetFromJsonAsync<IList<ProductDTO>>(
            $"{_uri}/products");

    public Task AddProduct(string name, int price) => 
        _client.GetStringAsync($"{_uri}/addProduct/{name}/{price}");

    public Task AddToCart(int id) => 
        _client.GetStringAsync($"{_uri}/addToCart/{id}");
    
    public Task DeleteFromCart(int id) => 
        _client.GetStringAsync($"{_uri}/deleteFromCart/{id}");
}