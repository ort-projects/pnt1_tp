using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

[Route("Pedido")]
public class PedidoController(PedidoService pedidoService, IMapper mapper) : Controller
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

    [HttpGet("Confirmacion")]
    public async Task<IActionResult> Confirmacion(int id)
    {
        var pedido = await pedidoService.ObtenerPedido(id);
        if (pedido is null) return NotFound();

        return View(mapper.Map<PedidoViewModel>(pedido));
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var pedidos = await pedidoService.ObtenerTodos();
        return View(mapper.Map<List<PedidoViewModel>>(pedidos));
    }

    [Authorize]
    [HttpGet("{id}/Detalle")]
    public async Task<IActionResult> Detalle(int id)
    {
        var pedidoItems = await pedidoService.ObtenerPedido(id);
        if ((pedidoItems?.PedidoProductos.Count ?? 0) == 0) return NotFound();
        return View(mapper.Map<List<PedidoItemViewModel>>(pedidoItems!.PedidoProductos));
    }
}
