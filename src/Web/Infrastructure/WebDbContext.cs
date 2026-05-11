using Microsoft.EntityFrameworkCore;
using Web.Domain;

namespace Web.Infrastructure;

public class WebDbContext : DbContext
{
    public WebDbContext(DbContextOptions options) : base(options)
    {
    }

    protected WebDbContext()
    {
    }

    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<Producto> Producto { get; set; }

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
        });

        modelBuilder.Entity<Categoria>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Nombre).IsUnique();
        });

        modelBuilder.Entity<Producto>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);
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
        });
    }
}