using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Catalog;

public class ClassCatalog
{
    private readonly ConcurrentBag<Product> _products = new()
    {
        new Product("Shirt", 100, ClassCategories.GetCategories().First(c => c.Id == 1)),
        new Product("Sneakers", 300, ClassCategories.GetCategories().First(c => c.Id == 2)),
        new Product("Hat", 200, ClassCategories.GetCategories().First(c => c.Id == 3)),
        new Product("Watches", 2800, ClassCategories.GetCategories().First(c => c.Id == 1)),
    };


    public ConcurrentBag<Product> GetProducts(DayOfWeek dayOfWeek, string userAgent)
    {
        if (dayOfWeek == DayOfWeek.Friday || userAgent.ToLower().Contains("iphone"))
            lock (_products)
            {
                return new ConcurrentBag<Product>(_products.Select(p =>
                {
                    p.Price += p.Price / 2;
                    return p;
                }));
            }

        if(userAgent.ToLower().Contains("android"))
            lock (_products)
            {
                return new ConcurrentBag<Product>(_products.Select(p =>
                {
                    p.Price -= p.Price / 10;
                    return p;
                }));
            }

        return _products;
    }

    public void AddProduct(Product product) => _products.Add(product);
}