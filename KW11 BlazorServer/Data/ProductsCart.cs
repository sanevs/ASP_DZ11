using Catalog;

namespace KW11_BlazorServer.Data;

public static class ProductsCart
{
    public static IList<Product> Cart { get; } = new List<Product>();
}