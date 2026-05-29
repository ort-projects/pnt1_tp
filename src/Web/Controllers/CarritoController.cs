using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

public class CarritoController(CarritoService carritoService, ProductoService productoService) : Controller
{
    private const string SessionKey = "CarritoId";
    private const string CountKey = "CarritoCount";

    public async Task<IActionResult> Index()
    {
        var carritoId = HttpContext.Session.GetInt32(SessionKey);
        if (carritoId is null) return View(new CarritoViewModel());

        var carrito = await carritoService.ObtenerCarrito(carritoId.Value);
        if (carrito is null) return View(new CarritoViewModel());

        var vm = new CarritoViewModel
        {
            Items = carrito.CarritoProductos.Select(cp => new CarritoItemViewModel
            {
                CarritoProductoId = cp.Id,
                ProductoId = cp.IdProducto,
                Nombre = cp.Producto.Nombre,
                UrlImagen = cp.Producto.UrlImagen,
                Cantidad = cp.Cantidad,
                PrecioUnitario = cp.PrecioUnitario,
                Subtotal = cp.Subtotal
            }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar(int productoId, int cantidad)
    {
        var producto = await productoService.GetProducto(productoId);
        if (producto is null) return NotFound();

        var carritoId = HttpContext.Session.GetInt32(SessionKey);
        if (carritoId is null)
        {
            var nuevoCarrito = await carritoService.CrearCarrito();
            carritoId = nuevoCarrito.Id;
            HttpContext.Session.SetInt32(SessionKey, carritoId.Value);
        }

        await carritoService.AgregarProducto(carritoId.Value, productoId, cantidad, producto.Precio);

        var carrito = await carritoService.ObtenerCarrito(carritoId.Value);
        HttpContext.Session.SetInt32(CountKey, carrito?.CarritoProductos.Sum(cp => cp.Cantidad) ?? 0);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar(int carritoProductoId)
    {
        await carritoService.EliminarProducto(carritoProductoId);

        var carritoId = HttpContext.Session.GetInt32(SessionKey);
        if (carritoId.HasValue)
        {
            var carrito = await carritoService.ObtenerCarrito(carritoId.Value);
            HttpContext.Session.SetInt32(CountKey, carrito?.CarritoProductos.Sum(cp => cp.Cantidad) ?? 0);
        }

        return RedirectToAction("Index");
    }
}
