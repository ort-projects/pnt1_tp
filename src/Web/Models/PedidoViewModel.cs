namespace Web.Models;

public class PedidoViewModel
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public double Total { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<PedidoItemViewModel> Items { get; set; } = [];
}

public class PedidoItemViewModel
{
    public string NombreProducto { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Subtotal { get; set; }
}
