namespace Web.Domain;

public class CarritoProducto
{
    public int Id { get; set; }
    public int IdCarrito { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Subtotal { get; set; }

    //Navigation
    public virtual Producto Producto { get; set; }
    public virtual Carrito Carrito { get; set; }

}