using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Services;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class AdminController(ProductoService productosService, IMapper mapper) : Controller
{
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        if (email != "admin@admin.com" || password != "admin")
        {
            ViewBag.Error = "Credenciales invalidas";
            return View("Login");
        }

        List<Claim> claims = new()
        {
            new Claim("Email", email)
        };

        ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Admin");
    }

    public async Task<IActionResult> Index()
    {
        var productos = await productosService.GetAllProductos();
        var productosModels = mapper.Map<List<ProductoModelAdmin>>(productos);
        return View(productosModels);
    }
}