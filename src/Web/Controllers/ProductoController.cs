using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

public class ProductoController(ProductoService productoService, IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var productos = await productoService.GetAllProductos();
        var viewModel = new ProductoListViewModel
        {
            Productos = mapper.Map<IList<ProductoModel>>(productos)
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Detalle(int id)
    {
        var producto = await productoService.GetProducto(id);
        if (producto == null) return NotFound();

        return View(mapper.Map<ProductoModel>(producto));
    }
}
