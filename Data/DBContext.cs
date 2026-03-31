using Microsoft.EntityFrameworkCore;
using MiApiBackend.Models;

namespace MiApiBackend.Data;
public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<Producto> Productos { get; set; } = null!;
    public DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
    public DbSet<Pedido> Pedidos { get; set; } = null!;
}