using System.Diagnostics;
using Glory.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorPagesWeb.Models;
using Microsoft.Extensions.DependencyInjection;

namespace RazorPagesWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalog _catalog;

    //private static ClassCatalog ClassCatalog { get; set; } = new ClassCatalog();

    public HomeController(ILogger<HomeController> logger, ICatalog catalog)
    {
        _logger = logger;
        _catalog = catalog;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult ShowCategories()
    {
        return View(_catalog.Categories.GetCategories());
    }
    public IActionResult AddProduct(string name, int price, string category)
    {

        if (HttpContext.Request.Method == "POST")
        {
            if (name is null)
                return View("Error");
            _catalog.AddProduct(new Product(
                name, 
                price, 
                _catalog.Categories.GetCategories().First(cat => cat.Name == category)
                ));
        }
        
        return View(_catalog.Categories.GetCategories().ToList());
    }

    public IActionResult AddCategory(string name)
    {
        if (HttpContext.Request.Method == "POST" && !(name is null))
        {
            _catalog.Categories.AddCategory(name); 
        }

        return View();
    }
    
    public IActionResult ShowProducts(string category = "default")
    {
        var catalog = _catalog
            .GetProducts(DateTime.Now.DayOfWeek, "Windows")
            .ToList();
        if (category == "default")
            return View(catalog);
        return View(catalog
            .Where(c => c.Category.Name == category)
            .ToList());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}