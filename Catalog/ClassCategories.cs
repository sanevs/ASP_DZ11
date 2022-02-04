using System.Collections.Concurrent;

namespace Catalog;

public static class ClassCategories
{
    private static readonly ConcurrentBag<Category> _categories = new()
    {
        new Category(1, "Men"),
        new Category(2, "Women"),
        new Category(3, "Kids"),
    };
    public static ConcurrentBag<Category> GetCategories() => _categories;
    public static void AddCategory(string name) => _categories.Add(new Category(GetMaxId() + 1, name));
    //public int GetMinId() => GetCategories().Select(c => c.Id).Min();
    private static int GetMaxId() => GetCategories().Select(c => c.Id).Max();
}