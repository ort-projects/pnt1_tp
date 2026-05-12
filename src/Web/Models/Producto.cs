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