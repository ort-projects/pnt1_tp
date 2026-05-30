namespace Web.Models;

public class ProductoModel
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public double Precio { get; set; }
    public string UrlImagen { get; set; } = string.Empty;
    public bool Destacado { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
}

public class EditProductModel : ProductoModel
{
    public IList<(int id, string nombre)> Categorias { get; set; }
}

public class ProductoModelAdmin : ProductoModel
{
    public string NombreCategoria { get; set; }
}

public class UpdateProductoModel
{
    public int? CategoriaId { get; set; }
    public string? Nombre { get; set; }
    public string? SKU { get; set; }
    public string? Descripcion { get; set; }
    public double? Precio { get; set; }
    public string? UrlImagen { get; set; }
    public bool? Destacado { get; set; }
    public bool? Estado { get; set; }
}