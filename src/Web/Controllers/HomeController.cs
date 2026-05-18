using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Domain;
using Web.Models;

namespace Web.Controllers;

public class HomeController(ProductoService productoService, CategoriaService categoriaService, IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var productos = await productoService.GetProductosDestacados(4);
        var categorias = await categoriaService.GetCategoriasWithImagen(4);
        ViewBag.Productos = mapper.Map<IList<Producto>>(productos);
        return View(new IndexModel
        {
            ProductosDestacados = mapper.Map<IList<ProductoModel>>(productos),
            Categorias = mapper.Map<IList<CategoriaModel>>(categorias)
        });
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
