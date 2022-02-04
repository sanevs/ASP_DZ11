using System.Collections.Concurrent;

namespace Catalog;

public interface ICatalog
{
    Categories Categories { get; }
    ConcurrentBag<Product> GetProducts(DayOfWeek dayOfWeek, string userAgent);
    void AddProduct(Product product);
}