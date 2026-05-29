using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

public class PedidoController(PedidoService pedidoService) : Controller
{
    private const string SessionKey = "CarritoId";
    private const string CountKey = "CarritoCount";

    [HttpPost]
    public async Task<IActionResult> Confirmar()
    {
        var carritoId = HttpContext.Session.GetInt32(SessionKey);
        if (carritoId is null) return RedirectToAction("Index", "Carrito");

        var pedido = await pedidoService.CrearPedido(carritoId.Value);
        if (pedido is null) return RedirectToAction("Index", "Carrito");

        HttpContext.Session.Remove(SessionKey);
        HttpContext.Session.SetInt32(CountKey, 0);

        return RedirectToAction(nameof(Confirmacion), new { id = pedido.Id });
    }

    public async Task<IActionResult> Confirmacion(int id)
    {
        var pedido = await pedidoService.ObtenerPedido(id);
        if (pedido is null) return NotFound();

        var vm = new PedidoViewModel
        {
            Id = pedido.Id,
            NumeroPedido = pedido.NumeroPedido,
            Total = pedido.Total,
            Estado = pedido.Estado,
            FechaCreacion = pedido.FechaCreacion,
            Items = pedido.PedidoProductos.Select(pp => new PedidoItemViewModel
            {
                NombreProducto = pp.NombreProducto,
                SKU = pp.SKU,
                Cantidad = pp.Cantidad,
                PrecioUnitario = pp.PrecioUnitario,
                Subtotal = pp.Subtotal
            }).ToList()
        };

        return View(vm);
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var pedidos = await pedidoService.ObtenerTodos();

        var vm = pedidos.Select(p => new PedidoViewModel
        {
            Id = p.Id,
            NumeroPedido = p.NumeroPedido,
            Total = p.Total,
            Estado = p.Estado,
            FechaCreacion = p.FechaCreacion,
            Items = p.PedidoProductos.Select(pp => new PedidoItemViewModel
            {
                NombreProducto = pp.NombreProducto,
                SKU = pp.SKU,
                Cantidad = pp.Cantidad,
                PrecioUnitario = pp.PrecioUnitario,
                Subtotal = pp.Subtotal
            }).ToList()
        }).ToList();

        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Cancelar(int id)
    {
        await pedidoService.CancelarPedido(id);
        return RedirectToAction(nameof(Index));
    }
}
