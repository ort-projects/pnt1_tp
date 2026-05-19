namespace Web.Models;

public class ProductoListViewModel
{
    public IList<ProductoModel> Productos { get; set; } = [];
    public IList<CategoriaModel> Categorias { get; set; } = [];
    public string? Search { get; set; }
    public int? CategoriaId { get; set; }
}
