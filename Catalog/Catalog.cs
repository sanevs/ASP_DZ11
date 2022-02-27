using System.Collections.Concurrent;
using System.Net.Http.Headers;

namespace Glory.Domain;

public class Catalog : ICatalog
{
    public Categories Categories { get; } = new Categories();
    private readonly ConcurrentBag<Product> _products;

    public Catalog()
    {
        _products = new()
        {
            new Product("Shirt", 100, Categories.GetCategories().First(c => c.Id == 1)),
            new Product("Sneakers", 300, Categories.GetCategories().First(c => c.Id == 2)),
            new Product("Hat", 200, Categories.GetCategories().First(c => c.Id == 3)),
            new Product("Watches", 2800, Categories.GetCategories().First(c => c.Id == 1)),
        };
    }


    public ConcurrentBag<Product> GetProducts(DayOfWeek dayOfWeek, string userAgent)
    {
        var products = new List<Product>(_products.Count);
        foreach (var p in _products)
        {
            products.Add((Product)p.Clone());
        }

        if (dayOfWeek == DayOfWeek.Friday || userAgent.ToLower().Contains("iphone"))
            lock (products)
            {
                return new ConcurrentBag<Product>(products.Select(p =>
                {
                    p.Price = p.Price * 3 / 2;
                    return p;
                }).ToList());
            }

        if(userAgent.ToLower().Contains("android"))
            lock (products)
            {
                return new ConcurrentBag<Product>(products.Select(p =>
                {
                    p.Price -= p.Price / 10;
                    return p;
                }).ToList());
            }

        return new ConcurrentBag<Product>(products);
    }
    
    public void AddProduct(Product product) => _products.Add(product);
    public Product GetProduct(string name) => _products.FirstOrDefault(p => p.Name == name);
}