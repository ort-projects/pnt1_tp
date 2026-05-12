using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Domain;
using Web.Models;

namespace Web.Controllers;

public class HomeController(ProductosService productosService, IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var productos = await productosService.GetProductosDestacados();
        ViewBag.Productos = mapper.Map<IList<Producto>>(productos);
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
