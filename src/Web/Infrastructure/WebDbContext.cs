using Microsoft.EntityFrameworkCore;
using Web.Domain;

namespace Web.Infrastructure;

public class WebDbContext : DbContext
{
    private DbSet<Carrito> _carritos { get; set; }
    private DbSet<Categoria> _categorias { get; set; }
    private DbSet<Pedido> _pedidos { get; set; }
    private DbSet<Producto> _productos { get; set; }

    public WebDbContext(DbContextOptions options) : base(options)
    {
    }

    protected WebDbContext()
    {
    }

    public IQueryable<Carrito> Carritos => _carritos;
    public IQueryable<Categoria> Categorias => _categorias;
    public IQueryable<Pedido> Pedidos => _pedidos;
    public IQueryable<Producto> Productos => _productos;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Carrito>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.IdSesion).IsUnique();

            builder
                .HasOne(c => c.Pedido)
                .WithOne(p => p.Carrito)
                .HasForeignKey<Pedido>(p => p.IdCarrito);

            builder.ToTable("Carritos");
            builder.HasQueryFilter(p => p.Estado);
        });

        modelBuilder.Entity<Categoria>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Nombre).IsUnique();
            builder.ToTable("Categorias");
        });

        modelBuilder.Entity<Producto>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);
            
            builder.ToTable("Productos");
            builder.HasQueryFilter(p => p.Estado);
        });

        modelBuilder.Entity<CarritoProducto>(builder =>
        {
            builder.HasKey(cp => cp.Id);

            builder
                .HasOne(cp => cp.Producto)
                .WithMany(p => p.CarritoProductos)
                .HasForeignKey(cp => cp.IdProducto);

            builder
                .HasOne(cp => cp.Carrito)
                .WithMany(p => p.CarritoProductos)
                .HasForeignKey(cp => cp.IdCarrito);

            builder.ToTable("CarritosProductos");

            builder.HasQueryFilter(c => c.Carrito.Estado);
        });


        modelBuilder.Entity<PedidoProducto>(builder =>
        {
            builder.HasKey(pp => pp.Id);

            builder
                .HasOne(pp => pp.Producto)
                .WithMany(p => p.PedidoProductos)
                .HasForeignKey(pp => pp.IdProducto);

            builder
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProductos)
                .HasForeignKey(pp => pp.IdPedido);

            builder.ToTable("PedidosProductos");

            builder.HasQueryFilter(pp => pp.Pedido.Estado && pp.Producto.Estado);
        });
        
        modelBuilder.Entity<Pedido>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder
                .HasMany(p => p.PedidoProductos)
                .WithOne(pp => pp.Pedido)
                .HasForeignKey(pp => pp.IdPedido);

            builder
                .HasOne(p => p.Carrito)
                .WithOne(c => c.Pedido)
                .HasForeignKey<Pedido>(p => p.IdCarrito);

            builder.ToTable("Pedidos");
            builder.HasQueryFilter(p => p.Carrito.Estado && p.Estado);
        });

    }
}