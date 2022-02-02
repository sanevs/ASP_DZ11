using Microsoft.AspNetCore.Mvc;

namespace RazorPagesWeb.Views.Home;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}