using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;

namespace Web.Application.Services;

public class PedidoService(WebDbContext db)
{
    public async Task<Pedido?> ObtenerPedido(int id)
    {
        return await db.Pedidos
            .Include(p => p.PedidoProductos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IList<Pedido>> ObtenerTodos()
    {
        return await db.Pedidos
            .IgnoreQueryFilters()
            .Include(p => p.PedidoProductos)
            .OrderByDescending(p => p.FechaCreacion)
            .ToListAsync();
    }

    public async Task<Pedido?> CrearPedido(int carritoId)
    {
        var carrito = await db.Carritos
            .Include(c => c.CarritoProductos)
            .ThenInclude(cp => cp.Producto)
            .FirstOrDefaultAsync(c => c.Id == carritoId);

        if (carrito is null || !carrito.CarritoProductos.Any()) return null;

        var numeroPedido = await db.Pedidos.IgnoreQueryFilters().CountAsync() + 1;
        var total = carrito.CarritoProductos.Sum(cp => cp.Subtotal);

        var pedido = new Pedido
        {
            IdCarrito = carritoId,
            NumeroPedido = numeroPedido,
            Total = total,
            Estado = true,
            FechaCreacion = DateTime.Now,
            FechaActualizacion = DateTime.Now
        };

        db.Add(pedido);
        await db.SaveChangesAsync();

        foreach (var cp in carrito.CarritoProductos)
        {
            db.Add(new PedidoProducto
            {
                IdPedido = pedido.Id,
                IdProducto = cp.IdProducto,
                NombreProducto = cp.Producto.Nombre,
                SKU = cp.Producto.SKU,
                Cantidad = cp.Cantidad,
                PrecioUnitario = cp.PrecioUnitario,
                Subtotal = cp.Subtotal
            });
        }

        await db.SaveChangesAsync();
        return pedido;
    }
}
