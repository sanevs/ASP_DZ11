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

    public Task<IList<ProductDTO>?> GetProducts() =>
        _client.GetFromJsonAsync<IList<ProductDTO>>(
            $"{_uri}/catalog/products");

    public Task AddProduct(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/catalog/addProduct", product);

    public Task AddToCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/addToCart", product);
    
    public Task DeleteFromCart(ProductDTO product) => 
        _client.PostAsJsonAsync($"{_uri}/cart/deleteFromCart", product);
}