using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;

namespace Web.Application.Services;

public class CarritoService(WebDbContext db)
{
    public async Task<Carrito?> ObtenerCarrito(int carritoId)
    {
        return await db.Carritos
            .Include(c => c.CarritoProductos)
            .ThenInclude(cp => cp.Producto)
            .FirstOrDefaultAsync(c => c.Id == carritoId);
    }

    public async Task<Carrito> CrearCarrito()
    {
        // IdSesion tiene unique index, usamos un negativo aleatorio para el primer save
        // y luego lo actualizamos al Id real (siempre positivo, nunca colisiona)
        var carrito = new Carrito
        {
            Estado = true,
            IdSesion = -Random.Shared.Next(1, int.MaxValue),
            FechaCreacion = DateTime.Now,
            FechaActualizacion = DateTime.Now
        };
        db.Add(carrito);
        await db.SaveChangesAsync();
        carrito.IdSesion = carrito.Id;
        await db.SaveChangesAsync();
        return carrito;
    }

    public async Task AgregarProducto(int carritoId, int productoId, int cantidad, double precio)
    {
        var carrito = await db.Carritos
            .Include(c => c.CarritoProductos)
            .FirstOrDefaultAsync(c => c.Id == carritoId);

        if (carrito is null) return;

        var item = carrito.CarritoProductos.FirstOrDefault(cp => cp.IdProducto == productoId);
        if (item is not null)
        {
            item.Cantidad += cantidad;
            item.Subtotal = item.Cantidad * item.PrecioUnitario;
        }
        else
        {
            db.Add(new CarritoProducto
            {
                IdCarrito = carritoId,
                IdProducto = productoId,
                Cantidad = cantidad,
                PrecioUnitario = precio,
                Subtotal = cantidad * precio
            });
        }

        carrito.FechaActualizacion = DateTime.Now;
        await db.SaveChangesAsync();
    }

    public async Task EliminarProducto(int carritoProductoId)
    {
        var item = await db.Set<CarritoProducto>().FindAsync(carritoProductoId);
        if (item is not null)
        {
            db.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}
