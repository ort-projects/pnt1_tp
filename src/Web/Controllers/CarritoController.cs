using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class CarritoController : Controller
{
    public IActionResult Index(int id)
    {
        var carrito = new Carrito()
        {
            Id = id
        };
        ViewBag.Id = id;
        return View(carrito);
    }
}