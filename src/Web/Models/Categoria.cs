namespace Web.Models;

public class CategoriaModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activa { get; set; }
    public string Url { get; set; }
}