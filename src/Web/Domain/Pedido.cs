namespace Web.Domain;

public class Pedido
{
    public int Id { get; set; }
    public int IdCarrito { get; set; }
    public int NumeroPedido { get; set; }
    public double Total { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    //Navigation
    public virtual Carrito Carrito { get; set; }
    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; }
}