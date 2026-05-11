namespace Web.Domain;

public class Carrito
{
    public int Id { get; set; }
    public int IdSesion { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    //Navigation
    public virtual Pedido? Pedido { get; set; } = null;
    public virtual ICollection<CarritoProducto> CarritoProductos { get; set; } = new List<CarritoProducto>();
}