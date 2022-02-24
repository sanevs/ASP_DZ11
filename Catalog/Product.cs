namespace Catalog;

public class Product
{
    public string Name { get; }
    public int Price { get; set; }
    public Category Category { get; }
    public string? ImageRef { get; set; }
    

    public Product(string name, int price, Category category)
    {
        Name = name;
        Price = price;
        Category = category;
        ImageRef = "product.jpg";
    }
}