namespace Web.Domain;

public class PedidoProducto
{
    public int Id { get; set; }
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Subtotal { get; set; }

    //Navigation
    public virtual Producto Producto { get; set; }
    public virtual Pedido Pedido { get; set; }
}