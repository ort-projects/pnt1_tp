using Microsoft.EntityFrameworkCore;
using Web.Domain;
using Web.Infrastructure;

namespace Web.Application.Services;

public class CategoriaService(WebDbContext webDbContext)
{
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
}