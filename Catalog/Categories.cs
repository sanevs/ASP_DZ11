using System.Collections.Concurrent;

namespace Catalog;

public class Categories
{
    private readonly ConcurrentBag<Category> _categories = new()
    {
        new Category(1, "Men"),
        new Category(2, "Women"),
        new Category(3, "Kids"),
    };
    public ConcurrentBag<Category> GetCategories() => _categories;
    public void AddCategory(string name) => _categories.Add(new Category(GetMaxId() + 1, name));
    private int GetMaxId() => GetCategories().Select(c => c.Id).Max();
}