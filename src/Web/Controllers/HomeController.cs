using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

public class HomeController(ProductosService productosService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var productos = await productosService.GetProductosDestacados();
        ViewBag.Productos = productos;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
