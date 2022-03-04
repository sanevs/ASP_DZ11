using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Glory.Domain;

namespace ShopClient;

public class MyClient
{
    private readonly string _uri;
    private readonly HttpClient _client;

    public MyClient(string uri, HttpClient client)
    {
        _uri = uri;
        _client = client;
    }

    public Task<List<Product>?> GetProducts() => _client.GetFromJsonAsync<List<Product>>($"{_uri}/products");
    public Task<HttpResponseMessage> GetResponseMessage() => _client.GetAsync($"{_uri}/products");
    public Task<string> AddProduct(string name, int price, int categoryId) => 
        _client.GetStringAsync($"{_uri}/add/{name}/{price}/{categoryId}");

    public void Print(List<Product>? products)
    {
        Console.WriteLine("*****Products:*****");
        if (products != null)
            foreach (var product in products)
            {
                Console.WriteLine(product.Name + $"({product.Category.Name}'s category), " + product.Price);
            }
    }
}