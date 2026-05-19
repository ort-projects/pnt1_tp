using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

[Route("Productos")]
public class ProductosController(ProductoService productoService, CategoriaService categoriaService, IMapper mapper) : Controller
{
    public async Task<IActionResult> Index(string? search, int? categoriaId)
    {
        var productos = await productoService.GetProductosFiltrados(search, categoriaId);
        var categorias = await categoriaService.GetAllCategorias();

        var viewModel = new ProductoListViewModel
        {
            Productos = mapper.Map<IList<ProductoModel>>(productos),
            Categorias = mapper.Map<IList<CategoriaModel>>(categorias),
            Search = search,
            CategoriaId = categoriaId
        };

        return View(viewModel);
    }

    [Route("Detalle/{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var producto = await productoService.GetProducto(id);
        if (producto == null) return NotFound();

        return View(mapper.Map<ProductoModel>(producto));
    }

    [Authorize]
    [Route("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var producto = await productoService.GetProducto(id, true);
        if (producto == null) return NotFound();
        var model = mapper.Map<EditProductModel>(producto);
        var categorias = await categoriaService.GetAllIdsAndNames();
        model.Categorias = categorias;
        return View(model);
    }

    [Authorize]
    [HttpPost("Update/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateProductoModel updateModel)
    {
        var producto = await productoService.GetProducto(id, true);
        if (producto is null) return NotFound();
        mapper.Map(updateModel, producto);
        await productoService.UpdateProducto(producto);
        return RedirectToAction("Edit", new { id });
    }
}
