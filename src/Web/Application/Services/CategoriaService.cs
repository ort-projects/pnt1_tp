using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;

namespace Web.Application.Services;

public class CategoriaService(WebDbContext webDbContext)
{
    public async Task<IList<Categoria>> GetAllCategorias()
    {
        return await webDbContext.Categorias
            .Where(c => c.Activa)
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }

    public async Task<IList<(Categoria, string)>> GetCategoriasWithImagen(int limit)
    {
        var categorias = await webDbContext.Categorias
            .Where(x => x.Productos.Any())
            .Select(x => new
            {
                categoria = x,
                producto = x.Productos
                    .OrderBy(p => p.Id)
                    .First()
            })
            .Take(limit)
            .ToListAsync();
        return categorias.Select(x => (x.categoria, x.producto.UrlImagen)).ToList();
    }

    public async Task<IList<(int id, string nombre)>> GetAllIdsAndNames()
    {
        return (await webDbContext.Categorias
            .Select(x => new { x.Id, x.Nombre })
            .ToListAsync()).Select(x => (x.Id, x.Nombre)).ToList();
    }
}