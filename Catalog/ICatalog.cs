using System.Collections.Concurrent;

namespace Glory.Domain;

public interface ICatalog
{
    Categories Categories { get; }
    ConcurrentBag<Product> GetProducts(DayOfWeek dayOfWeek, string userAgent);
    void AddProduct(Product product);
    Product GetProduct(string name);
}