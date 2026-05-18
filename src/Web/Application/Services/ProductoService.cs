using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;
namespace Web.Application.Services;

public class ProductoService(WebDbContext webDbContext)
{
    public async Task<IList<Producto>> GetAllProductos()
    {
        return await webDbContext.Productos.ToListAsync();
    }

    public async Task<IList<Producto>> GetProductosDestacados(int limit)
    {
        return await webDbContext.Productos
            .Where(p => p.Destacado)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IList<Producto>> GetProductosByCategoria(string nombreCategoria)
    {
        return await webDbContext.Productos
            .Where(p => p.Categoria.Nombre == nombreCategoria)
            .ToListAsync();
    }

    public async Task<IList<Producto>> GetProductosBySearch(string search)
    {
        return await webDbContext.Productos
            .Where(p => p.Categoria.Nombre.Contains(search) || p.Nombre.Contains(search) || p.Descripcion.Contains(search) || p.Nombre.Contains(search))
            .ToListAsync();
    }

    public async Task<Producto?> GetProducto(int id)
    {
        return await webDbContext.Productos.FirstOrDefaultAsync(p => p.Id == id);
    }
}