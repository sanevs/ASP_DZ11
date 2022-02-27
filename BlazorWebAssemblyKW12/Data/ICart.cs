using System.Collections.Concurrent;
using BlazorWebAssemblyKW12.Pages;
using Glory.Domain;

namespace BlazorWebAssemblyKW12.Data;

public interface ICart
{
    void AddProduct(Product product);
    void DeleteProduct(Product product);
    void Clear();
    IDictionary<Product, int> GetCartProducts();
    int GetCount();
}