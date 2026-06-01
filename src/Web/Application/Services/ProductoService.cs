using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;
namespace Web.Application.Services;

public class ProductoService(WebDbContext webDbContext)
{
    public async Task<IList<Producto>> GetAllProductos()
    {
        return await webDbContext.Productos
            .Include(x => x.Categoria)
            .ToListAsync();
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

    public async Task<IList<Producto>> GetProductosFiltrados(string? search, int? categoriaId)
    {
        var query = webDbContext.Productos.AsQueryable();

        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Nombre.Contains(search)
                                  || p.SKU.Contains(search)
                                  || p.Descripcion.Contains(search)
                                  || p.Categoria.Nombre.Contains(search));

        return await query.ToListAsync();
    }

    public async Task<Producto?> GetProducto(int id, bool ignoreStatus = false)
    {
        var queryable = webDbContext.Productos;
        if (ignoreStatus)
            queryable = queryable.IgnoreQueryFilters();
        return await queryable.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateProducto(Producto producto)
    {
        producto.FechaActualizacion = DateTime.Now;
        webDbContext.Update(producto);
        await webDbContext.SaveChangesAsync();
    }

    public async Task DeleteProducto(Producto producto)
    {
        await webDbContext.Productos.Where(p => p.Id == producto.Id).ExecuteDeleteAsync();
    }

    public async Task<Producto> AddProducto(Producto producto)
    {
        webDbContext.Add(producto);
        await webDbContext.SaveChangesAsync();
        return producto;
    }
}