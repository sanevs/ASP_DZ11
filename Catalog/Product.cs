using System.Net.Http.Headers;
using Microsoft.VisualBasic.CompilerServices;

namespace Glory.Domain;

public class Product : ICloneable
{
    public string Name { get; }
    public int Price { get; set; }
    public Category Category { get; set; }
    public string? ImageRef { get; set; }


    public Product(string name, int price, Category category)
    {
        Name = name;
        Price = price;
        Category = category;
        ImageRef = "product.jpg";
    }

    public object Clone() => new Product(Name, Price, Category);

    public static bool operator ==(Product left, Product right)
    {
        return left.Name == right.Name &&
               left.Price == right.Price &&
               left.Category == right.Category;
    }

    public static bool operator !=(Product left, Product right)
    {
        return left.Name != right.Name ||
               left.Price != right.Price ||
               left.Category != right.Category;
    }
}