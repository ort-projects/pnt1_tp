namespace Web.Models;

public class CarritoViewModel
{
    public IList<CarritoItemViewModel> Items { get; set; } = new List<CarritoItemViewModel>();
    public double Total => Items.Sum(i => i.Subtotal);
}

public class CarritoItemViewModel
{
    public int CarritoProductoId { get; set; }
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string UrlImagen { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Subtotal { get; set; }
}
